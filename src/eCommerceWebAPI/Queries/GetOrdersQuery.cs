using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Queries
{
    public record GetOrdersQuery: IRequest<List<OrderDTO>>;
}
