using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Services;
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using System.Text.Json;
using Xunit;

namespace EcommerceApp.Tests.Services
{
    public class CartServiceTests
    {
        private readonly Mock<IUnitOfWork> _uofMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpAccessorMock;
        private readonly Mock<ICartRepository> _cartRepoMock;
        private readonly Mock<ICartItemRepository> _cartItemRepoMock;
        private readonly Mock<ISession> _sessionMock;
        private readonly CartService _cartService;
        private byte[] _sessionValue;

        public CartServiceTests()
        {
            _uofMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _httpAccessorMock = new Mock<IHttpContextAccessor>();
            _cartRepoMock = new Mock<ICartRepository>();
            _cartItemRepoMock = new Mock<ICartItemRepository>();
            _sessionMock = new Mock<ISession>();

            _uofMock.Setup(u => u.CartRepository).Returns(_cartRepoMock.Object);
            _uofMock.Setup(u => u.CartItemRepository).Returns(_cartItemRepoMock.Object);

            var httpContext = new DefaultHttpContext();
            httpContext.Session = _sessionMock.Object;
            _httpAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            _sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out _sessionValue))
                        .Returns(() => _sessionValue != null);
            _sessionMock.Setup(s => s.Set(It.IsAny<string>(), It.IsAny<byte[]>()))
                        .Callback<string, byte[]>((key, value) => _sessionValue = value);
            _sessionMock.Setup(s => s.Remove(It.IsAny<string>()))
                        .Callback<string>((key) => _sessionValue = null);

            _sessionMock.Setup(s => s.TryGetValue(It.IsAny<string>(), out _sessionValue))
            .Returns((string key, out byte[] value) =>
            {
                value = _sessionValue;
                return _sessionValue != null;
            });

            _cartService = new CartService(_uofMock.Object, _mapperMock.Object, _httpAccessorMock.Object);
        }

        [Fact]
        public async Task GetCartAsync_ShouldReturnCartFromDatabase_WhenUserIsLoggedIn()
        {
            string userId = "user123";
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, userId)
            }, "mock"));
            _httpAccessorMock.Setup(x => x.HttpContext.User).Returns(user);

            var fakeCartEntity = new CartModel { ApplicationUserId = userId, Id = 1 };
            _cartRepoMock.Setup(x => x.GetCartWithItemsByUserIdAsync(userId))
                         .ReturnsAsync(fakeCartEntity);

            var fakeCartDto = new CartDTO();
            _mapperMock.Setup(x => x.Map<CartDTO>(fakeCartEntity))
                       .Returns(fakeCartDto);

            var result = await _cartService.GetCartAsync();

            result.Should().NotBeNull();
            result.Should().BeSameAs(fakeCartDto);
            _cartRepoMock.Verify(x => x.GetCartWithItemsByUserIdAsync(userId), Times.Once);
        }

        [Fact]
        public async Task MigrateCartAsync_ShouldMergeItems_WhenItemExistsInBothCarts()
        {
            string userId = "user123";
            int productId = 55;

            var sessionCart = new CartDTO { Itens = new List<CartItemDTO> { new CartItemDTO { ProductId = productId, Quantity = 1 } } };
            _sessionValue = JsonSerializer.SerializeToUtf8Bytes(sessionCart);

            var dbItem = new CartItemModel { ProductId = productId, Quantity = 2, Id = 100, CartId = 1 };
            var dbCart = new CartModel { Id = 1, ApplicationUserId = userId, Itens = new List<CartItemModel> { dbItem } };

            _cartRepoMock.Setup(x => x.GetCartWithItemsByUserIdAsync(userId))
                         .ReturnsAsync(dbCart);

            await _cartService.MigrateCartAsync(userId);

            _sessionMock.Verify(s => s.Remove(It.IsAny<string>()), Times.Once, "O carrinho da sessão não foi limpo, o que indica que o serviço não encontrou itens nele.");

            dbItem.Quantity.Should().Be(3);
            _cartItemRepoMock.Verify(x => x.Update(It.Is<CartItemModel>(item => item.ProductId == productId && item.Quantity == 3)), Times.Once);
            _uofMock.Verify(x => x.CommitAsync(), Times.Once);
            _sessionMock.Verify(x => x.Remove(It.IsAny<string>()), Times.Once);
        }
    }
}