using AutoMapper;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Configurations
{
    public class AutoMapperInitializer : Profile
    {
        public AutoMapperInitializer()
        {
            //Mapping (with enum value to string)

            // Cart Items
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
            CreateMap<CartItem, CreateCartItemDTO>().ReverseMap();

            // Order & Checkout
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<Order, CheckOutDTO>().ReverseMap();

            // User
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, CreateUserDTO>().ReverseMap();
        }
    }
}
