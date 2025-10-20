using EcommerceApp.Context;
using EcommerceApp.Models;
using EcommerceApp.Repositories.Interfaces;

namespace EcommerceApp.Repositories
{
    public class CategoryRepository : Repository<CategoryModel>, ICategory
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
    }
}
