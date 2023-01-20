using MediatR;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Queries;

namespace eCommerceWebAPI.Handlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public GetItemByIdHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<CartItem> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.GetCartItemEntity(request.Guid);
        }
    }
}
