using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Core.Entities
{
    public class CartItemModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public ProductModel Product { get; set; }
        public int CartId { get; set; }
        public CartModel Cart { get; set; }
    }
}
