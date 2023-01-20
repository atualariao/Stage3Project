using AutoMapper;
using MediatR;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Interface;
using S3E1.Queries;

namespace S3E1.Handlers
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
