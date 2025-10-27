using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Entities;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
