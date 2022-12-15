using MediatR;
using S3E1.Entities;

namespace S3E1.Commands
{
    public record DeleteCartItemCommand(Guid id) : IRequest<CartItem>;
    
}
