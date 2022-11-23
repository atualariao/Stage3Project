using MediatR;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class DeleteCartItemsHandler : IRequestHandler<DeleteCartItemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public DeleteCartItemsHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<CartItemEntity> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.DeleteItem(request.id);
        }
    }
}
