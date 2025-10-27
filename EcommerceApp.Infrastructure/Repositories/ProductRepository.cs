using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using EcommerceApp.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<ProductModel>> GetProductsByCategoryAsync(int id)
        {
            return await _context.Set<ProductModel>()
                                 .Include(p => p.Category)
                                 .Where(p => p.CategoryId == id)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public override async Task<IEnumerable<ProductModel>> GetAllAsync()
        {
            return await _context.Set<ProductModel>()
                                 .Include(p => p.Category)
                                 .AsNoTracking()
                                 .ToListAsync();
        }

        public override async Task<ProductModel?> GetByIdAsync(int id)
        {
            return await _context.Set<ProductModel>()
                                 .Include(p => p.Category)
                                 .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
