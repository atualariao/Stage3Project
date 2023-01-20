using MediatR;

using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Commands
{
    public record DeleteOrderCommand(Guid id) : IRequest<Order>;
}
