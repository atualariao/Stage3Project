
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Interface
{
    public interface IOrderRepository
    {
        public Task<List<Order>> GetOrders();
        public Task<Order> GetOrderById(Guid id);
        public Task<Order> UpdateOrder(Order orders);
        public Task<Order> DeleteOrderById(Guid id);
    }
}
