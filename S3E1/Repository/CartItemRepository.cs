using Dapper;
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

        public async Task<IEnumerable<CartItemEntity>> GetCartItems()
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
        public async Task Createitem(CartItemEntity itemEntity)
        {
            _appDataContext.CartItems.Add(itemEntity);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.CartItems.ToListAsync();
        }
    }
}
