using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Commands
{
    public record UpdateOrderCommand(OrderDTO OrderDTO) : IRequest<Order>;
}
