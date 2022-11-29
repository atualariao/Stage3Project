using Azure.Core;
using Dapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S3E1.Contracts;
using S3E1.Data;

using S3E1.Entities;
using System.Data;

namespace S3E1.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<CartItemRepository> _logger;
        private IDbConnection _connection;

        public CartItemRepository(DbContext dbContext, ILogger<CartItemRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<List<CartItemEntity>> GetCartItems()
        {
            var query = "SELECT * FROM CartItems";
            try
            {
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    var itemList = await connection.QueryAsync<CartItemEntity>(query);

                    _logger.LogInformation("Cart Item List retrieved from the database");
                    return itemList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<CartItemEntity> GetCartItemEntity(Guid id)
        {
            var query = $"SELECT * FROM CartItems WHERE ItemID = @id";

            try
            {
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    var cartItem = await connection.QuerySingleOrDefaultAsync<CartItemEntity>(query, new { id });

                    _logger.LogInformation("Cart Item retrieved from database, Guid: {0}", cartItem.ItemID.ToString().ToUpper());
                    return cartItem;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in retrieving cart item, Details: {0}", ex);
                throw;
            }
        }
        public async Task<CartItemEntity> Createitem(CartItemEntity cartItems)
        {
            try
            {
                var item = new CartItemEntity()
                {
                    ItemID = Guid.NewGuid(),
                    ItemName = cartItems.ItemName,
                    ItemPrice = cartItems.ItemPrice,
                    ItemStatus = "Pending"
                };

                _dbContext.Set<CartItemEntity>().Add(item);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("New Item Created in the Database, Object: {0}", JsonConvert.SerializeObject(item).ToUpper());
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Creating Cart Item Details: {0}", ex);
                throw;
            }
        }

        public async Task<CartItemEntity> Updateitem(CartItemEntity cartItems)
        {
            try
            {
                var item = await _dbContext.Set<CartItemEntity>().FindAsync(cartItems.ItemID);
                item.ItemID = cartItems.ItemID;
                item.ItemName = cartItems.ItemName;
                item.ItemPrice = cartItems.ItemPrice;

                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Cart Updated from database, object: {0}", JsonConvert.SerializeObject(item).ToUpper());
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Updating Cart Item Details: {0}", ex);
                throw;
            }
        }

        public async Task<CartItemEntity> DeleteItem(Guid id)
        {
            try
            {
                var item = _dbContext.Set<CartItemEntity>().Find(id);

                _dbContext.Set<CartItemEntity>().Remove(item);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Cart Item Has been Removed from the database, Guid: {0}", item.ItemID.ToString().ToUpper());
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Deleting Cart Item Details: {0}", ex);
                throw;
            }
        }
    }
}
