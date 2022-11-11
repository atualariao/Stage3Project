using MediatR;
using S3E1.Data;
using S3E1.Entities;
using S3E1.Data;

namespace S3E1.Queries
{
    public record GetItemsQuery : IRequest<List<CartItemEntity>>;
}
