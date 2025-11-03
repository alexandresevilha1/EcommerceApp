using EcommerceApp.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.Services.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync();
        Task AddItemAsync(int productId, int quantity = 1);
        Task RemoveItemAsync(int productId);
        Task ClearCartAsync();
        Task UpdateItemQuantityAsync(int productId, int newQuantity);
        Task MigrateCartAsync(string userId);
    }
}
