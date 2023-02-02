using MediatR;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.DTOs;

namespace eCommerceWebAPI.Commands
{
    public record CheckOutCommand(Guid userId) : IRequest<Order>;
}
