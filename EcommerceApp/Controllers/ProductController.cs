using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EcommerceApp.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _productService.GetAllAsync();
            return View(products);
        }

        public async Task<IActionResult> Create()
        {
            await LoadCategoriesViewBag();
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            await LoadCategoriesViewBag();
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = id;
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await LoadCategoriesViewBag();
            var product = await _productService.GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductDTO productDto)
        {
            if (ModelState.IsValid)
            {
                await _productService.CreateAsync(productDto);
                return RedirectToAction(nameof(Index));
            }
            await LoadCategoriesViewBag();
            return View(productDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                await _productService.UpdateAsync(id, productDTO);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = id;
                return View(productDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, ProductDTO productDTO)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task LoadCategoriesViewBag()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = new SelectList(categories, "Id", "Name");
        }
    }
}