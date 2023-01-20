using MediatR;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Queries
{
    public record GetOrdersByIdQuery(Guid id) :IRequest<Order>;
}
