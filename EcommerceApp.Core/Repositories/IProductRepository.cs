using EcommerceApp.Core.Entities;

namespace EcommerceApp.Core.Repositories
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(int id);
    }
}
