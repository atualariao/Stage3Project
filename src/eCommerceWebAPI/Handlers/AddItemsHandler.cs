using AutoMapper;
using MediatR;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;

namespace eCommerceWebAPI.Handlers
{
    public class AddItemsHandler : IRequestHandler<AddCartItemCommand, CartItem>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public AddItemsHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _cartItemRepository = cartItemRepository;
            _mapper = mapper;
        }

        public async Task<CartItem> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            var item = new CartItem()
            {
                ItemName = request.CartItems.ItemName,
                ItemPrice = request.CartItems.ItemPrice,
                CustomerID = request.Guid,
            };

            return await _cartItemRepository.Createitem(item);
        }
    }
}
