﻿
using S3E1.Entities;

namespace S3E1.IRepository
{
    public interface IOrderRepository
    {
        public Task<List<OrderEntity>> GetOrders();
        public Task<OrderEntity> GetOrderById(Guid id);
        public Task<OrderEntity> UpdateOrder(OrderEntity orders);
        public Task<OrderEntity> DeleteOrderById(Guid id);
    }
}