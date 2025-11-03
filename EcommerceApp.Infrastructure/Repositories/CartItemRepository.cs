
using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using System.Linq.Expressions;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class CartItemRepository : Repository<CartItemModel>, ICartItemRepository
    {
        public CartItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
