using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckOutCommand, OrderEntity>
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly ILogger<CheckoutHandler> _logger;

        public CheckoutHandler(ICheckoutRepository checkoutRepository, ILogger<CheckoutHandler> logger)
        {
            _logger = logger;
            _checkoutRepository = checkoutRepository;
        }

        public async Task<OrderEntity> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("New Order Checkout has been added in the database, Object: {0}", JsonConvert.SerializeObject(request.Orders).ToUpper());
            return await _checkoutRepository.Checkout( request.Orders);
        }
    }
}
