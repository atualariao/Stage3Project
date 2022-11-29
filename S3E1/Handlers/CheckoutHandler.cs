using MediatR;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckOutCommand, OrderEntity>
    {
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutHandler(ICheckoutRepository checkoutRepository) => _checkoutRepository = checkoutRepository;

        public async Task<OrderEntity> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            return await _checkoutRepository.Checkout(request.Orders);
        }
    }
}
