using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class OrderRepository : Repository<OrderModel>, IOrderRepository
    {
        public OrderRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(string userId)
        {
            return await _context.Orders
                .Where(o => o.ApplicationUserId == userId)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .AsNoTracking()
                .ToListAsync();
        }

    }
}

