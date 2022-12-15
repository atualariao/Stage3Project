using AutoMapper;
using S3E1.DTOs;
using S3E1.Entities;

namespace S3E1.Configurations
{
    public class AutoMapperInitializer : Profile
    {
        public AutoMapperInitializer()
        {
            // Cart Items
            CreateMap<CartItem, CartItemDTO>().ReverseMap()
                .ForMember(t => t.OrderStatus,
                s => s.ToString());
            CreateMap<CartItem, CreateCartItemDTO>().ReverseMap()
                .ForMember(t => t.OrderStatus,
                s => s.ToString());

            // Order & Checkout
            CreateMap<Order, OrderDTO>().ReverseMap()
                .ForMember(d => d.OrderStatus,
                op => op.ToString());
            CreateMap<Order, CheckOutDTO>().ReverseMap()
                .ForMember(d => d.OrderStatus,
                op => op.ToString());

            // User
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
        }
    }
}
