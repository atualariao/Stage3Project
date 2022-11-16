using S3E1.DTO;
using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface IOrderRepository
    {
        public Task<List<Orders>> GerOrders();
        public Task<OrderEntity> GetOrderById(Guid id);
        public Task<Orders> UpdateOrder(Orders orders);
        public Task<OrderEntity> DeleteOrderById(Guid id);
    }
}
