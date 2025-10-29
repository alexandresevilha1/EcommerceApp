using System.Diagnostics;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;

        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }
    }
}
