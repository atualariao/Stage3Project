using AutoMapper;
using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Queries;

namespace eCommerceWebAPI.Handlers
{
    public class GetOrdersHandler : IRequestHandler<GetOrdersQuery, List<OrderDTO>>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;

        public GetOrdersHandler(IOrderRepository orderRepository, IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        public async Task<List<OrderDTO>> Handle(GetOrdersQuery request, CancellationToken cancellationToken)
        {
            var entity =  await _orderRepository.GetOrders();
            var dto = _mapper.Map<List<OrderDTO>>(entity);
            return dto;
        }
    }
}
