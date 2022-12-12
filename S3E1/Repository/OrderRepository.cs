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

        public async Task<List<Order>> GetOrders()
        {
            try
            {
                var query = "SELECT * FROM Orders o INNER JOIN Users u ON o.UserPrimaryID = u.UserID INNER JOIN CartItems c ON o.PrimaryID = c.OrderPrimaryID";
                Dictionary<Guid, Order> orderDict = new();
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    var orders = (await connection.QueryAsync<Order, User, CartItem, Order>(query, (order, user, item) =>
                    {
                        if (!orderDict.TryGetValue(order.PrimaryID, out Order? orderEntity))
                        {
                            orderEntity = order;
                            orderEntity.CartItemEntity ??= new List<CartItem>();
                            orderDict.Add(order.PrimaryID, orderEntity);
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

        public async Task<Order> GetOrderById(Guid id)
        {
            try
            {
                var query = "SELECT * FROM Orders WHERE PrimaryID = @id;" +
                                "SELECT * FROM CartItems WHERE PrimaryID = @id";

                using (var connection = _dbContext.Database.GetDbConnection())
                using (var multi = await connection.QueryMultipleAsync(query, new { id }))
                {
                    var order = await multi.ReadSingleOrDefaultAsync<Order>();
                    if (order != null)
                        order.CartItemEntity = (await multi.ReadAsync<CartItem>()).ToList();

                    _logger.LogInformation("Order retrieved from the database, Guid: {0}", order.PrimaryID.ToString().ToUpper());
                    return order;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in retrieving cart item, Details: {0}", ex);
                throw;
            }
        }

        public async Task<Order> UpdateOrder(Order orders)
        {
            try
            {
                if (orders != null)
                {
                    var order = await _dbContext.Set<Order>().FindAsync(orders.PrimaryID);
                    order.PrimaryID = orders.PrimaryID;
                    order.UserPrimaryID = orders.UserPrimaryID;
                    order.User = orders.User;
                    order.OrderCreatedDate = orders.OrderCreatedDate;
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

        public async Task<Order> DeleteOrderById(Guid id)
        {
            try
            {
                var order = _dbContext.Set<Order>().Find(id);
                var cartitem = _dbContext.Set<CartItem>().Where(item => item.OrderPrimaryID == order.PrimaryID);
                foreach (var item in cartitem)
                {
                    _dbContext.Set<CartItem>().Remove(item);
                }

                _dbContext.Set<Order>().Remove(order);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Order has been removed from the database, Guid: {0}", order.PrimaryID.ToString().ToUpper());
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
