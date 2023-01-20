using MediatR;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Queries
{
    public record GetItemByIdQuery(Guid Guid) : IRequest<CartItem>;
}
