using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Enumerations;
using eCommerceWebAPI.Data;

namespace eCommerceWebAPI.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDataContext _dbContext;
        private readonly ILogger<CheckoutRepository> _logger;
        public CheckoutRepository(AppDataContext context, ILogger<CheckoutRepository> logger)
        {
            _logger = logger;
            _dbContext = context;
        }

        public async Task<Order> Checkout(Order orders)
        {
            try
            {
                var userOrder = _dbContext
                    .Orders
                    .FirstOrDefault(user => user.UserPrimaryID == orders.UserPrimaryID);
                var itemList = _dbContext
                    .CartItems
                    .Where(status => status.OrderStatus == OrderStatus.Pending)
                    .ToList();
                var orderList = _dbContext
                    .Orders
                    .Where(status => status.OrderStatus == OrderStatus.Pending && status.UserPrimaryID == orders.UserPrimaryID)
                    .ToList();
                var TotalPrice = itemList
                    .Sum(x => x.ItemPrice);
                if (orders != null)
                {
                    foreach (var order in orderList)
                    {
                        order.OrderTotalPrice = TotalPrice;
                        order.OrderStatus = OrderStatus.Processed;

                        _dbContext.Orders.Update(order);
                    }

                    foreach (var item in itemList)
                    {
                        item.OrderStatus = OrderStatus.Processed;

                        _dbContext.CartItems.Update(item);
                    }
                    await _dbContext.SaveChangesAsync();
                }

                _logger.LogInformation($"New Order Checkout has been added in the database, Object: {JsonConvert.SerializeObject(orders).ToUpper()}");
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in Checkout Order Details: {ex}");
                throw;
            }
        }
    }
}
