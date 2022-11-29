using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Entities;
using System.Data;

namespace S3E1.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<CheckoutRepository> _logger;
        private IDbConnection _connection;

        public CheckoutRepository(DbContext context, ILogger<CheckoutRepository> logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<OrderEntity> Checkout(OrderEntity orders)
        {
            try
            {
                var cartItems = _dbContext.Set<CartItemEntity>().ToList();

                var TotalPrice = _dbContext.Set<CartItemEntity>()
                                        .Where(item => item.ItemStatus == "Pending")
                                        .Sum(item => item.ItemPrice);

                var newItems = _dbContext.Set<CartItemEntity>().Where(item => item.ItemStatus == "Pending").ToList();

                var userOrder = new OrderEntity()
                {
                    OrderID = orders.OrderID,
                    UserOrderId = orders.UserOrderId,
                    OrderTotalPrice = TotalPrice,
                    OrderCreatedDate = DateTime.Now,
                    CartItemEntity = newItems

                };
                foreach (var item in cartItems)
                {
                    if (item.ItemStatus == "Pending")
                    {
                        item.ItemStatus = "Processed";
                    }
                }
                _dbContext.Set<OrderEntity>().Add(userOrder);
                _dbContext.SaveChanges();
                await _dbContext.Set<CartItemEntity>().ToListAsync();
                return userOrder;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Checkout Order Details: {0}", ex);
                throw;
            }
        }
    }
}
