
using S3E1.DTOs;
using S3E1.Entities;

namespace S3E1.Interface
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrders();
        public Task<Order> GetOrderById(Guid id);
        public Task<Order> UpdateOrder(Order orders);
        public Task<Order> DeleteOrderById(Guid id);
    }
}
