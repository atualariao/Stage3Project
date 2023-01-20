using MediatR;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Interface;

namespace S3E1.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public DeleteOrderHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public async Task<Order> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            return await _orderRepository.DeleteOrderById(request.id);
        }
    }
}
