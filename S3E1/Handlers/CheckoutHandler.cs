using AutoMapper;
using MediatR;
using S3E1.Commands;
using S3E1.Enumerations;
using S3E1.Entities;
using S3E1.Interface;
using S3E1.DTOs;

namespace S3E1.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckOutCommand, Order>
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IMapper _mapper;

        public CheckoutHandler(ICheckoutRepository checkoutRepository, IMapper mapper)
        {
            _checkoutRepository = checkoutRepository;
            _mapper = mapper;
        }

        public async Task<Order> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var orderCheckout = new OrderDTO()
            {
                UserPrimaryID = request.Orders.UserPrimaryID,
            };

            Order order = _mapper.Map<Order>(orderCheckout);
            return await _checkoutRepository.Checkout(order);
        }
    }
}
