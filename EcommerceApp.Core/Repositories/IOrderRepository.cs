using EcommerceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Core.Repositories
{
    public interface IOrderRepository : IRepository<OrderModel>
    {
        Task<IEnumerable<OrderModel>> GetOrdersByUserIdAsync(string userId);
    }
}
