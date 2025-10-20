using AutoMapper;
using EcommerceApp.Models;

namespace EcommerceApp.DTO.Mappings
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
            CreateMap<ProductModel, ProductDTO>().ReverseMap();
        }
    }
}
