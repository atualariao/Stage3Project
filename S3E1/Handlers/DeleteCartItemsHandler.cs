using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class DeleteCartItemsHandler : IRequestHandler<DeleteCartItemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger<DeleteCartItemsHandler> _logger;

        public DeleteCartItemsHandler(ICartItemRepository cartItemRepository, ILogger<DeleteCartItemsHandler> logger)
        {
            _logger = logger;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItemEntity> Handle(DeleteCartItemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cart Item Has been Removed from the database, Guid: {0}", request.id.ToString().ToUpper());
            return await _cartItemRepository.DeleteItem(request.id);
        }
    }
}
