using AutoMapper;
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
                _dbContext.Set<OrderEntity>().Add(orders);
                _dbContext.SaveChanges();

                _logger.LogInformation("New Order Checkout has been added in the database, Object: {0}", JsonConvert.SerializeObject(orders).ToUpper());
                return orders;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Checkout Order Details: {0}", ex);
                throw;
            }
        }
    }
}
