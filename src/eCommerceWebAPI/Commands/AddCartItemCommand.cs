using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Commands
{
    public record AddCartItemCommand(CreateCartItemDTO CartItems, Guid Guid) : IRequest<CartItem>;
}
