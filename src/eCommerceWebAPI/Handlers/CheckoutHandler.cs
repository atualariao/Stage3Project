using AutoMapper;
using MediatR;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.Enumerations;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.DTOs;

namespace eCommerceWebAPI.Handlers
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
            var newCommand = new CheckOutCommand(request.userId);
            var userId = newCommand.userId;

            return await _checkoutRepository.Checkout(userId);
        }
    }
}
