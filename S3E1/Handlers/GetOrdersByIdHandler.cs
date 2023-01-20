using MediatR;
using S3E1.Entities;
using S3E1.Interface;
using S3E1.Queries;

namespace S3E1.Handlers
{
    public class GetOrdersByIdHandler : IRequestHandler<GetOrdersByIdQuery, Order>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersByIdHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public Task<Order> Handle(GetOrdersByIdQuery request, CancellationToken cancellationToken)
        {
            return _orderRepository.GetOrderById(request.id);
        }
    }
}
