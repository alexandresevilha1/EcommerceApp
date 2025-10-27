using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Application.Services.Interfaces;
using EcommerceApp.Core.Repositories;
using EcommerceApp.Core.Entities;

namespace EcommerceApp.Application.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _uof;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork uof, IMapper mapper)
        {
            _uof = uof;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _uof.CategoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO?> GetByIdAsync(int id)
        {
            var category = await _uof.CategoryRepository.GetByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task CreateAsync(CategoryDTO categoryDto)
        {
            var category = _mapper.Map<CategoryModel>(categoryDto);
            await _uof.CategoryRepository.CreateAsync(category);
            await _uof.CommitAsync();
        }

        public async Task UpdateAsync(int id, CategoryDTO categoryDto)
        {
            var categoryDb = await _uof.CategoryRepository.GetByIdAsync(id);
            if (categoryDb != null)
            {
                _mapper.Map(categoryDto, categoryDb);
                await _uof.CommitAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var categoryDb = await _uof.CategoryRepository.GetByIdAsync(id);
            if (categoryDb != null)
            {
                _uof.CategoryRepository.Delete(categoryDb);
                await _uof.CommitAsync();
            }
        }
    }
}
