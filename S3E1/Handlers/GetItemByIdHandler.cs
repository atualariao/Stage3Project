using MediatR;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly ILogger<GetItemByIdHandler> _logger;

        public GetItemByIdHandler(ICartItemRepository cartItemRepository, ILogger<GetItemByIdHandler> logger)
        {
            _logger = logger;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<CartItemEntity> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cart Item retrieved from database, Guid: {0}", request.Guid.ToString().ToUpper());
            return await _cartItemRepository.GetCartItemEntity(request.Guid);
        }
    }
}
