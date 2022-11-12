using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;

namespace S3E1.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataConnectionContext _connectionContext;
        private readonly AppDataContext _appDataContext;

        public OrderRepository(DataConnectionContext connectionContext, AppDataContext appDataContext)
        {
            _connectionContext = connectionContext;
            _appDataContext = appDataContext;
        }

        public async Task<List<OrderEntity>> GerOrders()
        {
            var query = "SELECT * FROM Orders";

            using (var connection = _connectionContext.CreateConnection())
            {
                var orders = await connection.QueryAsync<OrderEntity>(query);

                return orders.ToList();
            }
        }

        public async Task<OrderEntity> GetOrderById(Guid id)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @id";

            using (var connection = _connectionContext.CreateConnection())
            {
                var order = await connection.QuerySingleOrDefaultAsync<OrderEntity>(query, new { id });
                return order;
            }
        }

        public async Task<OrderEntity> UpdateOrder(OrderEntity orderEntity)
        {
            var order = await _appDataContext.Orders.FindAsync(orderEntity.OrderID);
            order.OrderID = orderEntity.OrderID;

            await _appDataContext.SaveChangesAsync();

            return orderEntity;
        }

        public async Task<OrderEntity> DeleteOrderById(Guid id)
        {
            var order = _appDataContext.Orders.Find(id);

            _appDataContext.Orders.Remove(order);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.Orders.ToListAsync();

            return order;
        }
    }
}
