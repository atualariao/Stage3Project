using MediatR;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
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
