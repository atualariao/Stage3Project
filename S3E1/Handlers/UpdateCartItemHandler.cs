using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartitemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _repository;
        private readonly ILogger<UpdateCartItemHandler> _logger;
        public UpdateCartItemHandler(ICartItemRepository repository, ILogger<UpdateCartItemHandler> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public Task<CartItemEntity> Handle(UpdateCartitemCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Cart Updated from database, object: {0}", JsonConvert.SerializeObject(request.CartItems).ToUpper());
            return _repository.Updateitem(request.CartItems);
        }
    }
}
