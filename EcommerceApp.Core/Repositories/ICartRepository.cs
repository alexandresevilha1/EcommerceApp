using EcommerceApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Core.Repositories
{
    public interface ICartRepository : IRepository<CartModel>
    {
        Task<CartModel> GetCartWithItemsByUserIdAsync(string userId);
        Task<CartModel> GetCartByUserIdAsync(string userId);
    }
}
