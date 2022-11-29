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
        private readonly ILogger<AddItemsHandler> _logger;

        public AddItemsHandler(ICartItemRepository cartItemRepository, ILogger<AddItemsHandler> logger)
        {
            _logger = logger;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItemEntity> Handle(AddCartItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("New Item Created in the Database, Object: {0}", JsonConvert.SerializeObject(request.CartItems).ToUpper());
            return await _cartItemRepository.Createitem(request.CartItems);
        }
    }
}
