using EcommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Web.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var cart = await _cartService.GetCartAsync();
            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int productId, string returnUrl)
        {
            if (productId <= 0)
            {
                return BadRequest();
            }
            await _cartService.AddItemAsync(productId, 1);
            return LocalRedirect(returnUrl);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            await _cartService.RemoveItemAsync(productId);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int productId, int newQuantity)
        {
            if (newQuantity > 0)
            {
                await _cartService.UpdateItemQuantityAsync(productId, newQuantity);
            }
            else
            {
                await _cartService.RemoveItemAsync(productId);
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ClearCart()
        {
            await _cartService.ClearCartAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
