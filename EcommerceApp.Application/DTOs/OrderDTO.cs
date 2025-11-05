using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcommerceApp.Application.DTOs
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal OrderTotal { get; set; }

        public List<OrderItemDTO> OrderItems { get; set; } = new List<OrderItemDTO>();
    }
}
