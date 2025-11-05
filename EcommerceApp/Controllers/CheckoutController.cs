using EcommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EcommerceApp.Web.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICartService _cartService;

        public CheckoutController(IOrderService orderService, ICartService cartService)
        {
            _orderService = orderService;
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var cart = await _cartService.GetCartAsync();

            if (cart == null || !cart.Itens.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PlaceOrder()
        {
            try
            {
                var newOrder = await _orderService.CreateOrderFromCartAsync();

                return RedirectToAction(nameof(Success), new { orderId = newOrder.Id });
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Erro ao finalizar o pedido: {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Success(int orderId)
        {
            ViewBag.OrderId = orderId;
            return View();
        }
    }
}