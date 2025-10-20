using System.Diagnostics;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProduct _product;

        public HomeController(IProduct product)
        {
            _product = product;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _product.GetAllAsync();
            return View(products);
        }
    }
}
