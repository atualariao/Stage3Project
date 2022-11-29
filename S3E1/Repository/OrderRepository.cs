using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

using System.Data;

namespace S3E1.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<OrderRepository> _logger;
        private IDbConnection _connection;

        public OrderRepository(DbContext dbContext, ILogger<OrderRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<List<OrderEntity>> GetOrders()
        {
            var query = "SELECT * FROM Orders o INNER JOIN Users u ON o.UserOrderId = u.UserID";

            using (var connection = _dbContext.Database.GetDbConnection())
            {
                var cartItems = await connection.QueryAsync<OrderEntity, UserEntity, OrderEntity>(query, (order, user) =>
                {
                    order.User = user;
                    return order;
                },
                splitOn: "UserID");

                return cartItems.ToList();
            }
        }

        public async Task<OrderEntity> GetOrderById(Guid id)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @id;" +
                                "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

            using (var connection = _dbContext.Database.GetDbConnection())
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
            try
            {
                var order = await _dbContext.Set<OrderEntity>().FindAsync(orders.OrderID);
                order.OrderID = orders.OrderID;
                order.OrderTotalPrice = orders.CartItemEntity.Sum(item => item.ItemPrice);
                order.CartItemEntity = orders.CartItemEntity;

                await _dbContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Updating Order Details: {0}", ex);
                throw;
            }
        }

        public async Task<OrderEntity> DeleteOrderById(Guid id)
        {
            try
            {
                var order = _dbContext.Set<OrderEntity>().Find(id);
                var cartitem = _dbContext.Set<CartItemEntity>().Where(item => item.OrderEntityOrderID == order.OrderID);
                foreach (var item in cartitem)
                {
                    _dbContext.Set<CartItemEntity>().Remove(item);
                }

                _dbContext.Set<OrderEntity>().Remove(order);
                await _dbContext.SaveChangesAsync();

                return order;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Deleting Order Details: {0}", ex);
                throw;
            }
        }
    }
}
