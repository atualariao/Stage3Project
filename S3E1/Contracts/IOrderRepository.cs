using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface IOrderRepository
    {
        public Task<IEnumerable<OrderEntity>> GerOrders();
        public Task<OrderEntity> GetOrderById(Guid id);
    }
}
