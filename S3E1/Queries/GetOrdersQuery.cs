using MediatR;
using S3E1.DTOs;
using S3E1.Entities;

namespace S3E1.Queries
{
    public record GetOrdersQuery: IRequest<List<OrderDTO>>;
}
