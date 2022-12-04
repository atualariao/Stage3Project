using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S3E1.Entities;
using S3E1.IRepository;
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
            try
            {
                var query = "SELECT * FROM Orders o INNER JOIN Users u ON o.UserOrderId = u.UserID INNER JOIN CartItems c ON o.OrderID = c.OrderEntityOrderID";
                Dictionary<Guid, OrderEntity> orderDict = new();
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    var orders = (await connection.QueryAsync<OrderEntity, UserEntity, CartItemEntity, OrderEntity>(query, (order, user, item) =>
                    {
                        if (!orderDict.TryGetValue(order.OrderID, out OrderEntity? orderEntity))
                        {
                            orderEntity = order;
                            orderEntity.CartItemEntity ??= new List<CartItemEntity>();
                            orderDict.Add(order.OrderID, orderEntity);
                        }

                        if (item is not null) orderEntity.CartItemEntity.Add(item);
                        order.User = user;

                        return orderEntity;
                    },
                    splitOn: "UserID, ItemID")).Distinct().ToList();

                    _logger.LogInformation("Order list retrieved from database");
                    return orders;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in retrieving Order list, Details: {0}", ex);
                throw;
            }
        }

        public async Task<OrderEntity> GetOrderById(Guid id)
        {
            try
            {
                var query = "SELECT * FROM Orders WHERE OrderID = @id;" +
                                "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

                using (var connection = _dbContext.Database.GetDbConnection())
                using (var multi = await connection.QueryMultipleAsync(query, new { id }))
                {
                    var order = await multi.ReadSingleOrDefaultAsync<OrderEntity>();
                    if (order != null)
                        order.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

                    _logger.LogInformation("Order retrieved from the database, Guid: {0}", order.OrderID.ToString().ToUpper());
                    return order;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in retrieving cart item, Details: {0}", ex);
                throw;
            }
        }

        public async Task<OrderEntity> UpdateOrder(OrderEntity orders)
        {
            try
            {
                if (orders != null)
                {
                    var order = await _dbContext.Set<OrderEntity>().FindAsync(orders.OrderID);
                    order.OrderID = orders.OrderID;
                    order.OrderTotalPrice = orders.CartItemEntity.Sum(item => item.ItemPrice);
                    order.CartItemEntity = orders.CartItemEntity;

                    await _dbContext.SaveChangesAsync();
                }


                _logger.LogInformation("Order Updated from database, Object: {0}", JsonConvert.SerializeObject(orders).ToUpper());
                return orders;
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

                _logger.LogInformation("Order has been removed from the database, Guid: {0}", order.OrderID.ToString().ToUpper());
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
