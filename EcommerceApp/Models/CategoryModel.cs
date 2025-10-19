namespace EcommerceApp.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public ICollection<ProductModel> Produtos { get; set; } = new List<ProductModel>();
    }
}
