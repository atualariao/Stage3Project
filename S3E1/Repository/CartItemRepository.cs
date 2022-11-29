using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

                    return cartItem;
                }
            }
            catch (Exception ex)
            {
                throw ex;
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
