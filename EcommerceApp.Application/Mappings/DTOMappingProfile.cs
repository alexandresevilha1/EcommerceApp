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

            CreateMap<CartModel, CartDTO>().ReverseMap();

            CreateMap<CartItemModel, CartItemDTO>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ProductUrlImage,
                           opt => opt.MapFrom(src => src.Product.ImageURL))
                .ForMember(dest => dest.ProductPrice,
                           opt => opt.MapFrom(src => src.Product.Price));

            CreateMap<CartItemDTO, CartItemModel>();

            CreateMap<OrderItemModel, OrderItemDTO>()
                .ForMember(dest => dest.ProductName,
                           opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.ImageUrl,
                           opt => opt.MapFrom(src => src.Product.ImageURL));

            CreateMap<OrderModel, OrderDTO>();
        }
    }
}
