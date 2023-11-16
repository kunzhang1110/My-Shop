using AutoMapper;
using MyShop.Entities;
using MyShop.DTOs;
using MyShop.Entities.OrderAggregate;

namespace MyShop.RequestHelpers
{
    /// <summary>
    /// Defines all AutoMapper mappings used in the application.  
    /// </summary>
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<CreateProductDto, Product>();
            CreateMap<UpdateProductDto, Product>();
            CreateMap<BasketItem, BasketItemDto>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Product.Name))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Product.Price))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.Product.PictureUrl))
                .ForMember(dest => dest.Brand, opt => opt.MapFrom(src => src.Product.Brand))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Product.Type));
            CreateMap<Basket, BasketDto>();
            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ItemOrdered.ProductId))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.ItemOrdered.Name))
                .ForMember(dest => dest.PictureUrl, opt => opt.MapFrom(src => src.ItemOrdered.PictureUrl));
            CreateMap<Order, OrderDto>();

        }
    }
}