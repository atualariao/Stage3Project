using MediatR;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.DTOs;

namespace eCommerceWebAPI.Commands
{
    public record CheckOutCommand(CheckOutDTO Orders) : IRequest<Order>;
}
