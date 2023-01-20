using AutoMapper;
using MediatR;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Interface;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class GetItemsHandler : IRequestHandler<GetItemsQuery, List<CartItemDTO>>
    {
        private readonly ICartItemRepository _cartItemRepository;
        private readonly IMapper _mapper;

        public GetItemsHandler(ICartItemRepository cartItemRepository, IMapper mapper)
        {
            _mapper = mapper;
            _cartItemRepository = cartItemRepository;
        }
        public async Task<List<CartItemDTO>> Handle(GetItemsQuery request, CancellationToken cancellationToken)
        {
            var items = await _cartItemRepository.GetCartItems();
            var dto = _mapper.Map<List<CartItemDTO>>(items);
            return dto;
        }
    }
}
