using MediatR;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand, OrderEntity>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<DeleteOrderHandler> _logger;

        public DeleteOrderHandler(IOrderRepository orderRepository, ILogger<DeleteOrderHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<OrderEntity> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order has been removed from the database, Guid: {0}", request.id.ToString().ToUpper());
            return await _orderRepository.DeleteOrderById(request.id);
        }
    }
}
