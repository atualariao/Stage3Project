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
        private readonly DbContext _dbContext;

        public CheckoutHandler(ICheckoutRepository checkoutRepository, IMapper mapper, DbContext dbContext)
        {
            _mapper = mapper;
            _checkoutRepository = checkoutRepository;
            _dbContext = dbContext;
        }

        public async Task<OrderEntity> Handle(CheckOutCommand request, CancellationToken cancellationToken)
        {
            var cartItems = _dbContext.Set<CartItemEntity>().ToList();

            var TotalPrice = _dbContext.Set<CartItemEntity>()
                                    .Where(item => item.ItemStatus == "Pending")
                                    .Sum(item => item.ItemPrice);

            var newItems = _dbContext.Set<CartItemEntity>().Where(item => item.ItemStatus == "Pending").ToList();

            var userOrder = new OrderEntity()
            {
                OrderID = Guid.NewGuid(),
                UserOrderId = request.Orders.UserOrderId,
                OrderTotalPrice = TotalPrice,
                OrderCreatedDate = DateTime.Now,
                CartItemEntity = newItems

            };
            foreach (var item in cartItems)
            {
                if (item.ItemStatus == "Pending")
                {
                    item.ItemStatus = "Processed";
                }
            }
            return await _checkoutRepository.Checkout(userOrder);
        }
    }
}
