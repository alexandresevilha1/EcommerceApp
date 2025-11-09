using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Services;
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Security.Claims;
using Xunit;

namespace EcommerceApp.Tests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IUnitOfWork> _uofMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly Mock<IHttpContextAccessor> _httpAccessorMock;

        private readonly OrderService _orderService;

        public OrderServiceTests()
        {
            _uofMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _httpAccessorMock = new Mock<IHttpContextAccessor>();

            _orderService = new OrderService(_uofMock.Object, _mapperMock.Object, _httpAccessorMock.Object);
        }

        [Fact]
        public async Task CreateOrderFromCartAsync_ShouldCreateOrderAndClearCart_WhenUserHasItems()
        {
            var cartRepoMock = new Mock<ICartRepository>();
            var orderRepoMock = new Mock<IOrderRepository>();
            var cartItemRepoMock = new Mock<ICartItemRepository>();

            _uofMock.Setup(u => u.CartRepository).Returns(cartRepoMock.Object);
            _uofMock.Setup(u => u.OrderRepository).Returns(orderRepoMock.Object);
            _uofMock.Setup(u => u.CartItemRepository).Returns(cartItemRepoMock.Object);

            string userId = "user123";

            var user = new ClaimsPrincipal(new ClaimsIdentity(new[] { new Claim(ClaimTypes.NameIdentifier, userId) }, "mock"));
            var httpContext = new DefaultHttpContext { User = user };
            _httpAccessorMock.Setup(x => x.HttpContext).Returns(httpContext);

            var fakeProduct = new ProductModel { Id = 10, Name = "Produto Teste", Price = 100m };
            var fakeCartItem = new CartItemModel { ProductId = 10, Product = fakeProduct, Quantity = 2 };
            var fakeCart = new CartModel { ApplicationUserId = userId, Itens = new List<CartItemModel> { fakeCartItem } };

            cartRepoMock.Setup(x => x.GetCartWithItemsByUserIdAsync(userId))
                        .ReturnsAsync(fakeCart);

            orderRepoMock.Setup(x => x.CreateAsync(It.IsAny<OrderModel>()))
                         .ReturnsAsync((OrderModel order) => order);

            _uofMock.Setup(x => x.CartRepository.GetCartWithItemsByUserIdAsync(userId))
                    .ReturnsAsync(fakeCart);
            
            _uofMock.Setup(x => x.OrderRepository.CreateAsync(It.IsAny<OrderModel>()))
                    .ReturnsAsync((OrderModel order) => order);

            _mapperMock.Setup(m => m.Map<OrderDTO>(It.IsAny<OrderModel>()))
                       .Returns(new OrderDTO { Id = 999, OrderTotal = 200m });

            var result = await _orderService.CreateOrderFromCartAsync();

            result.Should().NotBeNull();

            result.OrderTotal.Should().Be(200m);

            _uofMock.Verify(x => x.OrderRepository.CreateAsync(It.IsAny<OrderModel>()), Times.Once);

            _uofMock.Verify(x => x.CartItemRepository.Delete(fakeCartItem), Times.Once);

            _uofMock.Verify(x => x.CommitAsync(), Times.Once);
        }
    }
}