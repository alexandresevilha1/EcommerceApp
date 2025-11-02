using EcommerceApp.Application.DTOs;

namespace EcommerceApp.Application.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<ProductDTO?> GetByIdAsync(int id);
        Task<IEnumerable<ProductDTO>> GetProductsByCategoryAsync(int categoryId);
        Task<IEnumerable<ProductDTO>> SearchAsync(string query);
        Task CreateAsync(ProductDTO productDto);
        Task UpdateAsync(int id, ProductDTO productDto);
        Task DeleteAsync(int id);
    }
}
