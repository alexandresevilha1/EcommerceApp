using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.DTOs
{
    public class CartDTO
    {
        public List<CartItemDTO> Itens { get; set; } = new List<CartItemDTO>();
        public decimal TotalCarrinho => Itens.Sum(i => i.Subtotal);
    }
}
