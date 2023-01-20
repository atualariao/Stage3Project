using MediatR;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;

namespace eCommerceWebAPI.Handlers
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
