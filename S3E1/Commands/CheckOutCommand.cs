using MediatR;
using S3E1.Entities;
using S3E1.DTOs;

namespace S3E1.Commands
{
    public record CheckOutCommand(CheckOutDTO Orders) : IRequest<Order>;
}
