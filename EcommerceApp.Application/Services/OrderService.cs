using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public OrderService(IUnitOfWork uof, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _uof = uof;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<OrderDTO> CreateOrderFromCartAsync()
        {
            // 1. Obter o ID do usuário logado
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                throw new Exception("Usuário não está logado. Checkout não permitido.");
            }

            var cart = await _uof.CartRepository.GetCartWithItemsByUserIdAsync(userId);
            if (cart == null || !cart.Itens.Any())
            {
                throw new Exception("Seu carrinho está vazio.");
            }

            var order = new OrderModel
            {
                ApplicationUserId = userId,
                OrderDate = DateTime.UtcNow,
                OrderTotal = 0
            };

            foreach (var cartItem in cart.Itens)
            {
                var orderItem = new OrderItemModel
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = cartItem.Product.Price
                };

                order.OrderItems.Add(orderItem);

                order.OrderTotal += (orderItem.Price * orderItem.Quantity);
            }

            await _uof.OrderRepository.CreateAsync(order);

            foreach (var item in cart.Itens)
            {
                _uof.CartItemRepository.Delete(item);
            }

            await _uof.CommitAsync();

            order.OrderItems = order.OrderItems.ToList();
            return _mapper.Map<OrderDTO>(order);
        }

        public async Task<IEnumerable<OrderDTO>> GetOrdersForCurrentUserAsync()
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
            {
                return new List<OrderDTO>();
            }

            var orders = await _uof.OrderRepository.GetOrdersByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }

        private string GetUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}