using MediatR;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class GetItemsHandler : IRequestHandler<GetItemsQuery, List<CartItemEntity>>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger<GetItemsHandler> _logger;

        public GetItemsHandler(ICartItemRepository cartItemRepository, ILogger<GetItemsHandler> logger)
        {
            _logger = logger;
            _cartItemRepository = cartItemRepository;
        }

        public GetItemsHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<List<CartItemEntity>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cart Item List retrieved from the database");
            return await _cartItemRepository.GetCartItems();
        }
    }
}
