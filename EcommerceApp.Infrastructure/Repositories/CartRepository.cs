using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class CartRepository : Repository<CartModel>, ICartRepository
    {
        public CartRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<CartModel> GetCartWithItemsByUserIdAsync(string userId)
        {
             return await _context.Carts
                 .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }

        public async Task<CartModel> GetCartByUserIdAsync(string userId)
        {
            return await _context.Carts
                .Include(c => c.Itens)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.ApplicationUserId == userId);
        }
    }
}
