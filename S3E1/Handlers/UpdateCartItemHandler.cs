﻿using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class UpdateCartItemHandler : IRequestHandler<UpdateCartitemCommand, CartItemEntity>
    {
        private readonly ICartItemRepository _repository;
        public UpdateCartItemHandler(ICartItemRepository repository) => _repository = repository;

        public Task<CartItemEntity> Handle(UpdateCartitemCommand request, CancellationToken cancellationToken)
        {
            return _repository.Updateitem(request.CartItems);
        }
    }
}
