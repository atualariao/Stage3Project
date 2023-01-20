using AutoMapper;
using MediatR;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Interface;

namespace S3E1.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public UpdateOrderHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _mapper = mapper;
            _orderRepository = orderRepository;
        }
        public async Task<Order> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var orderUpdate = new OrderDTO()
            {
                PrimaryID = request.OrderDTO.PrimaryID,
                UserPrimaryID = request.OrderDTO.UserPrimaryID,
                OrderTotalPrice = request.OrderDTO.CartItemEntity.Sum(item => item.ItemPrice),
                CartItemEntity = request.OrderDTO.CartItemEntity
            };

            Order order = _mapper.Map<Order>(orderUpdate);
            return await _orderRepository.UpdateOrder(order);
        }
    }
}
