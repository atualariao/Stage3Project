using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using S3E1.DTO;

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

        public async Task<List<OrderEntity>> GetOrders()
        {
            var query = "SELECT * FROM Orders";

            using (var connection = _connectionContext.CreateConnection())
            {
                var cartItems = await connection.QueryAsync<OrderEntity>(query);

                return cartItems.ToList();
            }
        }

        public async Task<OrderEntity> GetOrderById(Guid id)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @id;" +
                                "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

            using (var connection = _connectionContext.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var order = await multi.ReadSingleOrDefaultAsync<OrderEntity>();
                if (order != null)
                    order.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

                return order;
            }
        }

        public async Task<OrderEntity> UpdateOrder(OrderEntity orders)
        {
            var order = await _appDataContext.Orders.FindAsync(orders.OrderID);
            order.OrderID = orders.OrderID;
            order.OrderTotalPrice = orders.CartItemEntity.Sum(item => item.ItemPrice);
            order.CartItemEntity = orders.CartItemEntity;

            await _appDataContext.SaveChangesAsync();

            return order;
        }

        public async Task<OrderEntity> DeleteOrderById(Guid id)
        {
            var order = _appDataContext.Orders.Find(id);
            var cartitem = _appDataContext.CartItems.Where(item => item.OrderEntityOrderID == order.OrderID);
            foreach (var item in cartitem)
            {
                _appDataContext.CartItems.Remove(item);
            }

            _appDataContext.Orders.Remove(order);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.Orders.ToListAsync();

            return order;
        }
    }
}
