using AutoMapper;
using MediatR;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartitemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _repository;
        private readonly IMapper _mapper;
        public UpdateCartItemHandler(ICartItemRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<CartItemEntity> Handle(UpdateCartitemCommand request, CancellationToken cancellationToken)
        {
            var item = new CartItemDTO()
            {
                ItemID = request.CartItems.ItemID,
                ItemName = request.CartItems.ItemName,
                ItemPrice = request.CartItems.ItemPrice
            };

            CartItemEntity cartItemEntity = _mapper.Map<CartItemEntity>(item);
            return await _repository.Updateitem(cartItemEntity);
        }
    }
}
