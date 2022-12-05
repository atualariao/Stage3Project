using AutoMapper;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;
using System.Data;

namespace S3E1.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<CheckoutRepository> _logger;
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
                foreach (var item in cartItems)
                {
                    if (item.ItemStatus == "Pending")
                    {
                        item.ItemStatus = "Processed";
                    }
                }
                var userOrder = new OrderEntity()
                {
                    UserOrderId = orders.UserOrderId,
                    OrderTotalPrice = TotalPrice,
                    CartItemEntity = newItems,

                };
                _dbContext.Set<OrderEntity>().Add(userOrder);
                _dbContext.SaveChanges();

                _logger.LogInformation("New Order Checkout has been added in the database, Object: {0}", JsonConvert.SerializeObject(userOrder).ToUpper());
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
