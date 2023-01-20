using MediatR;


using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Queries
{
    public record GetUserByIdQuery(Guid Guid) : IRequest<User>;
}
