using MediatR;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<OrderEntity>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrdersHandler> _logger;

        public GetOrdersHandler(IOrderRepository orderRepository, ILogger<GetOrdersHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public GetOrdersHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public Task<List<OrderEntity>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order list retrieved from database");
            return _orderRepository.GetOrders();
        }
    }
}
