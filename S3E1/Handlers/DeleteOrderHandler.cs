using MediatR;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, OrderEntity>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<OrderEntity> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteOrderById(request.id);
        }
    }
}
