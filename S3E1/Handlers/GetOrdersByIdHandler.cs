using MediatR;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Queries;

namespace eCommerceWebAPI.Handlers
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
