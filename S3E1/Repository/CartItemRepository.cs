using AutoMapper;
using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S3E1.Entities;
using S3E1.IRepository;
using S3E1.Enumerations;
using S3E1.Data;

namespace S3E1.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly AppDataContext _dbContext;
        private readonly ILogger<CartItemRepository> _logger;
        private readonly IMapper _mapper;

        public CartItemRepository(AppDataContext dbContext, ILogger<CartItemRepository> logger, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<List<CartItem>> GetCartItems()
        {
            var query = "SELECT * FROM CartItems";
            try
            {
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    var itemList = await connection.QueryAsync<CartItem>(query);

                    _logger.LogInformation("Cart Item List retrieved from the database");
                    return itemList.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public async Task<CartItem> GetCartItemEntity(Guid id)
        {
            var query = $"SELECT * FROM CartItems WHERE ItemID = @id";

            try
            {
                using (var connection = _dbContext.Database.GetDbConnection())
                {
                    var cartItem = await connection.QuerySingleOrDefaultAsync<CartItem>(query, new { id });

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
        public async Task<CartItem> Createitem(CartItem cartItems)
        {
            try
            {

                var user = _dbContext
                    .Users
                    .FirstOrDefault();
                var userOrder = _dbContext
                    .Orders
                    .FirstOrDefault(userOrder => userOrder.UserPrimaryID == user.UserID && userOrder.OrderStatus == OrderStatus.Pending);
                var itemlist = _dbContext
                    .CartItems
                    .Where(status => status.OrderStatus == OrderStatus.Pending)
                    .ToList();
                itemlist.Add(cartItems);
                var totalPrice = itemlist
                    .Sum(x => x.ItemPrice);
                if (userOrder != null && userOrder.OrderStatus == OrderStatus.Pending)
                {
                        userOrder.OrderTotalPrice = totalPrice;
                        userOrder.CartItemEntity = itemlist;

                        _dbContext.Orders.Update(userOrder);
                }
                else
                {
                    var order = new Order()
                    {
                        UserPrimaryID = user.UserID,
                        OrderTotalPrice = cartItems.ItemPrice,
                        OrderStatus = OrderStatus.Pending,
                        CartItemEntity = itemlist
                    };

                    _dbContext.Orders.Add(order);
                }
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

        public async Task<CartItem> Updateitem(CartItem cartItems)
        {
            try
            {
                if (cartItems != null)
                {
                    var item = await _dbContext.CartItems.FindAsync(cartItems.ItemID);
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

        public async Task<CartItem> DeleteItem(Guid id)
        {
            try
            {
                var item = _dbContext.CartItems.Find(id);

                _dbContext.CartItems.Remove(item);
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
