using MediatR;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.DTOs;

namespace eCommerceWebAPI.Commands
{
    public record CheckOutCommand : IRequest<Order>
    {
        public Guid UserId { get; set; }
    }
}
