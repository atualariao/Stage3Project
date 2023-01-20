using MediatR;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;

namespace eCommerceWebAPI.Handlers
{
    public class DeleteCartItemsHandler : IRequestHandler<DeleteCartItemCommand, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public DeleteCartItemsHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<CartItem> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.DeleteItem(request.id);
        }
    }
}
