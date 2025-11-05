using EcommerceApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderFromCartAsync();
        Task<IEnumerable<OrderDTO>> GetOrdersForCurrentUserAsync();
    }
}
