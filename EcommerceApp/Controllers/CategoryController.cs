using EcommerceApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApp.Controllers
{
    public class CategoryController
    {
        private readonly IUnitOfWork _uof;

        public CategoryController(IUnitOfWork uof)
        {
            _uof = uof;
        }

        public async Task<IActionResult> Index()
        {
            var categorias = await _uof.CategoryRepository.GetAllAsync();
            return View(categorias); // Passa a lista de CategoriaDto para a View
        }
    }
}
