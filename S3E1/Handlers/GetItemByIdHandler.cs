﻿using MediatR;
using S3E1.Entities;
using S3E1.IRepository;
using S3E1.Queries;

namespace S3E1.Handlers
{
    public class GetItemByIdHandler : IRequestHandler<GetItemByIdQuery, CartItemEntity>
    {
        private readonly ICartItemRepository _cartItemRepository;

        public GetItemByIdHandler(ICartItemRepository cartItemRepository) => _cartItemRepository = cartItemRepository;

        public async Task<CartItemEntity> Handle(GetItemByIdQuery request, CancellationToken cancellationToken)
        {
            return await _cartItemRepository.GetCartItemEntity(request.Guid);
        }
    }
}
