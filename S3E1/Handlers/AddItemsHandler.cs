using AutoMapper;
using MediatR;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class AddItemsHandler : IRequestHandler<AddCartItemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public AddItemsHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<CartItemEntity> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = new CartItemEntity()
            {
                ItemName = request.CartItems.ItemName,
                ItemPrice = request.CartItems.ItemPrice,
            };

            return await _cartItemRepository.Createitem(item);
        }
    }
}
