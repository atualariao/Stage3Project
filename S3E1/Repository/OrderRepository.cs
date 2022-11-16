using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
            var query = "SELECT * FROM Orders WHERE OrderID = @id;" +
                                "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

            using(var connection = _connectionContext.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var order =  await multi.ReadSingleOrDefaultAsync<OrderEntity>();
                if (order != null)
                    order.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

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
            var cartitems = _appDataContext.CartItems.ToList();
            foreach (var item in cartitems)
            {
                item.ItemStatus = "Pending";
            }

            _appDataContext.Orders.Remove(order);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.Orders.ToListAsync();

            return order;
        }
    }
}
