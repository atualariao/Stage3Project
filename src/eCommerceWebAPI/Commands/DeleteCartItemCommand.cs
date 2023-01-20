using MediatR;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Commands
{
    public record DeleteCartItemCommand(Guid id) : IRequest<CartItem>;
    
}
