using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Commands
{
    public record UpdateCartitemCommand(CartItemDTO CartItems) : IRequest<CartItem>;
}
