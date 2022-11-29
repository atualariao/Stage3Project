using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;

namespace S3E1.Handlers
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderCommand, OrderEntity>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<UpdateOrderHandler> _logger;
        public UpdateOrderHandler(IOrderRepository orderRepository, ILogger<UpdateOrderHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public async Task<OrderEntity> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order Updated from database, Object: {0}", JsonConvert.SerializeObject(request.Orders).ToUpper());
            return await _orderRepository.UpdateOrder(request.Orders);
        }
    }
}
