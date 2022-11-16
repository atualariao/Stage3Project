using MediatR;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<Orders>>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrdersHandler(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        public Task<List<Orders>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            return _orderRepository.GerOrders();
        }
    }
}
