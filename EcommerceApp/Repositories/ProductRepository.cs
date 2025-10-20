using EcommerceApp.Context;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;

namespace EcommerceApp.Repositories
{
    public class ProductRepository : Repository<ProductModel>, IProduct
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(int id)
        {
            var products = await GetAllAsync();
            var productsByCategory = products.Where(p => p.CategoryId == id);
            return productsByCategory;
        }
    }
}
