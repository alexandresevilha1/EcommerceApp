using System.ComponentModel.DataAnnotations;

namespace EcommerceApp.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; } // 3. Adicione o ID

        [Required(ErrorMessage = "O nome da categoria é obrigatório")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [StringLength(200)]
        public string Description { get; set; } = string.Empty;
    }
}
