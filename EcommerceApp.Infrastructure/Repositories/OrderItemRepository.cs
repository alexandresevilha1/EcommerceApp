using EcommerceApp.Core.Entities;
using EcommerceApp.Core.Repositories;
using EcommerceApp.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Infrastructure.Repositories
{
    public class OrderItemRepository : Repository<OrderItemModel>, IOrderItemRepository
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
