using Microsoft.AspNetCore.Mvc;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            ViewBag.CategoryId = id;
            return View(category);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.CreateAsync(categoryDTO);
                return RedirectToAction(nameof(Index));
            }
            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _categoryService.UpdateAsync(id, categoryDTO);
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.CategoryId = id;
                return View(categoryDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, CategoryDTO categoryDTO)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
