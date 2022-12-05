using MediatR;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderEntity>
    {
        private readonly IOrderRepository _orderRepository;
        public UpdateOrderHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<OrderEntity> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderUpdate = new OrderEntity()
            {
                OrderID = request.Orders.OrderID,
                UserOrderId = request.Orders.UserOrderId,
                OrderCreatedDate = request.Orders.OrderCreatedDate,
                OrderTotalPrice = request.Orders.CartItemEntity.Sum(item => item.ItemPrice),
                CartItemEntity = request.Orders.CartItemEntity
            };

            return await _orderRepository.UpdateOrder(orderUpdate);
        }
    }
}
