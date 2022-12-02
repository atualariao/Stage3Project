using AutoMapper;
using S3E1.Entities;
using S3E1.DTOs;

namespace S3E1.Profiles
{
    public class Profiles : Profile
    {
        public Profiles()
        {
            CreateMap<CartItemEntity, CartItemDTO>().ReverseMap();
            CreateMap<OrderEntity, OrderDTO>().ReverseMap();
            CreateMap<UserEntity, UserDTO>().ReverseMap();
        }
    }
}
