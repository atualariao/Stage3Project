using MediatR;


using S3E1.Entities;

namespace S3E1.Queries
{
    public record GetUserByIdQuery(Guid Guid) : IRequest<User>;
}
