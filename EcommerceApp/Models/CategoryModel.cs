namespace EcommerceApp.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public ICollection<ProductModel> Produtos { get; set; } = new List<ProductModel>();
    }
}
