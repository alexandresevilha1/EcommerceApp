using AutoMapper;
using EcommerceApp.Application.DTOs;
using EcommerceApp.Core.Entities;

namespace EcommerceApp.Application.Mappings
{
    public class DTOMappingProfile : Profile
    {
        public DTOMappingProfile()
        {
            CreateMap<CategoryModel, CategoryDTO>().ReverseMap();

            CreateMap<ProductModel, ProductDTO>()
                .ForMember(dest => dest.CategoryName,
                           opt => opt.MapFrom(src => src.Category.Name));

            CreateMap<ProductDTO, ProductModel>();
        }
    }
}
