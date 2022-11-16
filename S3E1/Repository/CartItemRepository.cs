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
        public async Task<CartItemEntity> Createitem(CartItemEntity itemEntity)
        {
            var item = new CartItemEntity()
            {
                ItemID = Guid.NewGuid(),
                ItemName = itemEntity.ItemName,
                ItemPrice = itemEntity.ItemPrice,
            };

            _appDataContext.CartItems.Add(item);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.CartItems.ToListAsync();

            return itemEntity;
        }

        public async Task<CartItemEntity> Updateitem(CartItemEntity itemEntity)
        {
            var item = await _appDataContext.CartItems.FindAsync(itemEntity.ItemID);
            item.ItemID = itemEntity.ItemID;
            item.ItemName = itemEntity.ItemName;
            item.ItemPrice = itemEntity.ItemPrice;

            await _appDataContext.SaveChangesAsync();

            return itemEntity;
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
