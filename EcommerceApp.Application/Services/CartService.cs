using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Extensions;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace EcommerceApp.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private const string CartsSessionKey = "CartId";

        public CartService(IUnitOfWork uof, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uof = uof;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CartDTO> GetCartAsync()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return GetSessionCart();
            }

            var cartEntity = await _uof.CartRepository.GetCartWithItemsByUserIdAsync(userId);
            return _mapper.Map<CartDTO>(cartEntity);
        }

        public async Task AddItemAsync(int productId, int quantity = 1)
        {
            var product = await _uof.ProductRepository.GetByIdAsync(productId);
            if (product == null) throw new Exception("Produto não encontrado.");

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                var cart = GetSessionCart();
                var existingItem = cart.Itens.FirstOrDefault(i => i.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                }
                else
                {
                    cart.Itens.Add(_mapper.Map<CartItemDTO>(product));
                }
                SaveSessionCart(cart);
            }
            else
            {
                var cart = await GetOrCreateDbCartAsync(userId);
                var existingItem = cart.Itens.FirstOrDefault(i => i.ProductId == productId);

                if (existingItem != null)
                {
                    existingItem.Quantity += quantity;
                    _uof.CartItemRepository.Update(existingItem);
                }
                else
                {
                    var newItem = new CartItemModel
                    {
                        Id = cart.Id,
                        ProductId = productId,
                        Quantity = quantity
                    };
                    await _uof.CartItemRepository.CreateAsync(newItem);
                }
                await _uof.CommitAsync();
            }
        }

        public async Task RemoveItemAsync(int productId)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                var cart = GetSessionCart();
                var item = cart.Itens.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    cart.Itens.Remove(item);
                    SaveSessionCart(cart);
                }
            }
            else
            {
                var cart = await _uof.CartRepository.GetCartWithItemsByUserIdAsync(userId);
                if (cart == null) return;

                var item = cart.Itens.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    _uof.CartItemRepository.Delete(item);
                    await _uof.CommitAsync();
                }
            }
        }

        public async Task ClearCartAsync()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                ClearSessionCart();
            }
            else
            {
                var cart = await _uof.CartRepository.GetCartWithItemsByUserIdAsync(userId);
                if (cart != null && cart.Itens.Any())
                {
                    foreach (var item in cart.Itens)
                    {
                        _uof.CartItemRepository.Delete(item);
                    }
                    await _uof.CommitAsync();
                }
            }
        }

        public async Task UpdateItemQuantityAsync(int productId, int newQuantity)
        {
            if (newQuantity <= 0)
            {
                await RemoveItemAsync(productId);
                return;
            }

            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                var cart = GetSessionCart();
                var item = cart.Itens.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    item.Quantity = newQuantity;
                    SaveSessionCart(cart);
                }
            }
            else
            {
                var cart = await _uof.CartRepository.GetCartWithItemsByUserIdAsync(userId);
                var item = cart?.Itens.FirstOrDefault(i => i.ProductId == productId);
                if (item != null)
                {
                    item.Quantity = newQuantity;
                    _uof.CartItemRepository.Update(item);
                    await _uof.CommitAsync();
                }
            }
        }

        public async Task MigrateCartAsync(string userId)
        {
            var sessionCart = GetSessionCart();
            if (sessionCart == null || !sessionCart.Itens.Any()) return;

            var dbCart = await GetOrCreateDbCartAsync(userId);

            foreach (var sessionItem in sessionCart.Itens)
            {
                var dbItem = dbCart.Itens.FirstOrDefault(i => i.ProductId == sessionItem.ProductId);
                if (dbItem != null)
                {
                    dbItem.Quantity += sessionItem.Quantity;
                    _uof.CartItemRepository.Update(dbItem);
                }
                else
                {
                    var newItem = new CartItemModel
                    {
                        Id = dbCart.Id,
                        ProductId = sessionItem.ProductId,
                        Quantity = sessionItem.Quantity
                    };
                    await _uof.CartItemRepository.CreateAsync(newItem);
                }
            }

            await _uof.CommitAsync();
            ClearSessionCart();
        }

        private ISession GetSession() => _httpContextAccessor.HttpContext.Session;

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        private CartDTO GetSessionCart()
        {
            var cart = GetSession().Get<CartDTO>(CartsSessionKey);
            if (cart == null)
            {
                cart = new CartDTO();
                SaveSessionCart(cart);
            }
            return cart;
        }

        private void SaveSessionCart(CartDTO cart)
        {
            GetSession().Set(CartsSessionKey, cart);
        }

        private void ClearSessionCart()
        {
            GetSession().Remove(CartsSessionKey);
        }

        private async Task<CartModel> GetOrCreateDbCartAsync(string userId)
        {
            var cart = await _uof.CartRepository.GetCartWithItemsByUserIdAsync(userId);
            if (cart == null)
            {
                cart = new CartModel { ApplicationUserId = userId };
                await _uof.CartRepository.CreateAsync(cart);
                await _uof.CommitAsync();
            }
            return cart;
        }
    }
}
