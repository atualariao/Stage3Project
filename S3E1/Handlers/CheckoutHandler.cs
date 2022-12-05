using AutoMapper;
using MediatR;
using S3E1.Commands;
using Microsoft.EntityFrameworkCore;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class CheckoutHandler : IRequestHandler<CheckOutCommand, OrderEntity>
    {
        private readonly ICheckoutRepository _checkoutRepository;
        private readonly IMapper _mapper;

        public CheckoutHandler(ICheckoutRepository checkoutRepository, IMapper mapper)
        {
            _mapper = mapper;
            _checkoutRepository = checkoutRepository;
        }

        public async Task<OrderEntity> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            OrderEntity orderDTO = _mapper.Map<OrderEntity>(request.Orders);
            return await _checkoutRepository.Checkout(orderDTO);
        }
    }
}
