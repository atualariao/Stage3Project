using Dapper;
using MediatR;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.DTO;
using S3E1.Entities;
using System.Data;

namespace S3E1.Handlers
{
    public class AddItemsHandler : IRequestHandler<AddCartItemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public AddItemsHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<CartItemEntity> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            return await  _cartItemRepository.Createitem(request.CartItems);
        }
    }
}
