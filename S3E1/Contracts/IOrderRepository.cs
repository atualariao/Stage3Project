using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface IOrderRepository
    {
        public Task<List<OrderEntity>> GerOrders();
        public Task<OrderEntity> GetOrderById(Guid id);
        public Task<OrderEntity> UpdateOrder(OrderEntity orderEntity);
        public Task<OrderEntity> DeleteOrderById(Guid id);
    }
}
