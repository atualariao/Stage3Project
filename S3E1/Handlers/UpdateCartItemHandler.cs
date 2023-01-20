using AutoMapper;
using MediatR;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Interface;

namespace S3E1.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartitemCommand, CartItem>
    {
        private readonly ICartItemRepository _repository;
        private readonly IMapper _mapper;
        public UpdateCartItemHandler(ICartItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CartItem> Handle(UpdateCartitemCommand request, CancellationToken cancellationToken)
        {
            var item = new CartItemDTO()
            {
                ItemID = request.CartItems.ItemID,
                ItemName = request.CartItems.ItemName,
                ItemPrice = request.CartItems.ItemPrice
            };

            CartItem cartItemEntity = _mapper.Map<CartItem>(item);
            return await _repository.Updateitem(cartItemEntity);
        }
    }
}
