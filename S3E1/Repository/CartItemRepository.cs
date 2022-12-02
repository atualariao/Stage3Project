using AutoMapper;
using Azure.Core;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S3E1.Data;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;
using System.Data;

namespace S3E1.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<CartItemRepository> _logger;
        private readonly IMapper _mapper;

        public CartItemRepository(DbContext dbContext, ILogger<CartItemRepository> logger, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
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
                _dbContext.Set<CartItemEntity>().Add(cartItems);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("New Item Created in the Database, Object: {0}", JsonConvert.SerializeObject(cartItems).ToUpper());
                return cartItems;
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
                if (cartItems != null)
                {
                    var item = await _dbContext.Set<CartItemEntity>().FindAsync(cartItems.ItemID);
                    item.ItemName = cartItems.ItemName;
                    item.ItemPrice = cartItems.ItemPrice;

                    await _dbContext.SaveChangesAsync();
                }
                    _logger.LogInformation("Cart Updated from database, object: {0}", JsonConvert.SerializeObject(cartItems).ToUpper());
                    return cartItems;
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
