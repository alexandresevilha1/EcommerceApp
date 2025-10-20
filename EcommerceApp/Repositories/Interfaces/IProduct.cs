using EcommerceApp.Models;

namespace EcommerceApp.Repositories.Interfaces
{
    public interface IProduct : IRepository<ProductModel>
    {
        Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(int id);
    }
}
