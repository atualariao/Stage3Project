using MediatR;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.DTO;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Handlers
{
    public class GetItemsHandler : IRequestHandler<GetItemsQuery, List<CartItemEntity>>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public GetItemsHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<List<CartItemEntity>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.GetCartItems();
        }
    }
}
