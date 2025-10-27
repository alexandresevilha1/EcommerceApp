using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO?> GetByIdAsync(int id);
        Task CreateAsync(CategoryDTO categoryDto);
        Task UpdateAsync(int id, CategoryDTO categoryDto);
        Task DeleteAsync(int id);
    }
}
