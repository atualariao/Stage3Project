using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class AddItemsHandler : IRequestHandler<AddCartItemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public AddItemsHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<CartItemEntity> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.Createitem(request.CartItems);
        }
    }
}
