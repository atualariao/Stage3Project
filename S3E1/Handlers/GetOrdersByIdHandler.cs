using MediatR;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class GetOrdersByIdHandler : IRequestHandler<GetOrdersByIdQuery, OrderEntity>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ILogger<GetOrdersByIdHandler> _logger;

        public GetOrdersByIdHandler(IOrderRepository orderRepository, ILogger<GetOrdersByIdHandler> logger)
        {
            _logger = logger;
            _orderRepository = orderRepository;
        }

        public Task<OrderEntity> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Order retrieved from the database, Guid: {0}", request.id.ToString().ToUpper());
            return _orderRepository.GetOrderById(request.id);
        }
    }
}
