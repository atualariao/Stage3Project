using AutoMapper;
using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Queries;
using eCommerceWebAPI.Repository;

namespace eCommerceWebAPI.Handlers
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
