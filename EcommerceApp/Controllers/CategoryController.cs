using AutoMapper;
using EcommerceApp.DTO;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EcommerceApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoryController(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _uof.CategoryRepository.GetAllAsync();
            return View(categories);
        }

        public async Task<IActionResult> Register()
        {
            return View();
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _uof.CategoryRepository.GetAsync(c => c.Id == id);
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            ViewBag.CategoryId = id;
            return View(categoryDTO);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _uof.CategoryRepository.GetAsync(c => c.Id == id);
            var categoryDTO = _mapper.Map<CategoryDTO>(category);
            ViewBag.CategoryId = id;
            return View(categoryDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Register(CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var category = _mapper.Map<CategoryModel>(categoryDTO);
                var ctgr = _uof.CategoryRepository.Create(category);
                _uof.Commit();

                return RedirectToAction("Index", "Category");
            }
            else
            {
                return View(categoryDTO);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Update(int id, CategoryDTO categoryDTO)
        {
            if (ModelState.IsValid)
            {
                var categoryDb = await _uof.CategoryRepository.GetAsync(c => c.Id  == id);
                var category = _mapper.Map(categoryDTO, categoryDb);
                _uof.Commit();

                return RedirectToAction("Index", "Category");
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
            if (ModelState.IsValid)
            {
                var categoryDb = await _uof.CategoryRepository.GetAsync(c => c.Id == id);
                _uof.CategoryRepository.Delete(categoryDb);
                _uof.Commit();

                return RedirectToAction("Index", "Category");
            }
            else
            {
                ViewBag.CategoryId = id;
                return View(categoryDTO);
            }
        }
    }
}
