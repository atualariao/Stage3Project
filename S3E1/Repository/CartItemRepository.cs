using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.DTO;
using S3E1.Entities;
using System.Data;

namespace S3E1.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataConnectionContext _connectionContext;
        private readonly AppDataContext _appDataContext;

        public CartItemRepository(DataConnectionContext connectionContext, AppDataContext appDataContext)
        {
            _connectionContext = connectionContext;
            _appDataContext = appDataContext;
        }

        public async Task<List<CartItemEntity>> GetCartItems()
        {
            var query = "SELECT * FROM CartItems";

            using (var connection = _connectionContext.CreateConnection())
            {
                var cartItems = await connection.QueryAsync<CartItemEntity>(query);

                return cartItems.ToList();
            }
        }
        public async Task<CartItemEntity> GetCartItemEntity(Guid id)
        {
            var query = "SELECT * FROM CartItems WHERE ItemID = @id";

            using (var connection = _connectionContext.CreateConnection())
            {
                var cartItem = await connection.QuerySingleOrDefaultAsync<CartItemEntity>(query, new { id });

                return cartItem;
            }
        }
        public async Task<CartItemEntity> Createitem(CartItemEntity cartItems)
        {
            var item = new CartItemEntity()
            {
                ItemID = Guid.NewGuid(),
                ItemName = cartItems.ItemName,
                ItemPrice = cartItems.ItemPrice,
                ItemStatus = "Pending"
            };

            _appDataContext.CartItems.Add(item);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.CartItems.ToListAsync();

            return item;
        }

        public async Task<CartItemEntity> Updateitem(CartItemEntity cartItems)
        {
            var item = await _appDataContext.CartItems.FindAsync(cartItems.ItemID);
            item.ItemID = cartItems.ItemID;
            item.ItemName = cartItems.ItemName;
            item.ItemPrice = cartItems.ItemPrice;

            await _appDataContext.SaveChangesAsync();

            return item;
        }

        public async Task<CartItemEntity> DeleteItem(Guid id)
        {
            var item = _appDataContext.CartItems.Find(id);

            _appDataContext.CartItems.Remove(item);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.CartItems.ToListAsync();

            return item;
        }
    }
}
