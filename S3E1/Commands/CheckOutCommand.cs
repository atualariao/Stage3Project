using MediatR;
using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace S3E1.Commands
{
    public record CheckOutCommand(OrderEntity OrderEntity) : IRequest<OrderEntity>;
}
