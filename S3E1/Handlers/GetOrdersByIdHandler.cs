using MediatR;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Handlers
{
    public class GetOrdersByIdHandler : IRequestHandler<GetOrdersByIdQuery, OrderEntity>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByIdHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public Task<OrderEntity> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
        {
            return _orderRepository.GetOrderById(request.id);
        }
    }
}
