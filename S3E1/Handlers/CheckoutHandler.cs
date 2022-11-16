using MediatR;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace S3E1.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckOutCommand, Orders>
    {
        private readonly ICheckoutRepository _checkoutRepository;

        public CheckoutHandler(ICheckoutRepository checkoutRepository)
        {
            _checkoutRepository = checkoutRepository;
        }

        public async Task<Orders> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            return await _checkoutRepository.Checkout( request.Orders);
        }
    }
}
