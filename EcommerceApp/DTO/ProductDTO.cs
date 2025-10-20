using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceApp.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        [Column(TypeName = "decimal(18,2)")]
        [Range(0.01, (double)decimal.MaxValue, ErrorMessage = "O preço mínimo deve ser maior que zero.")]//annotation to require price to be greater than zero.
        public decimal Price { get; set; }

        public string? ImageURL { get; set; }
        public int Stock { get; set; }


        public int CategoryId { get; set; }
    }
}
