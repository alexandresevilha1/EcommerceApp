using System.Diagnostics;
using EcommerceApp.Core.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductRepository _product;

        public HomeController(IProductRepository product)
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
