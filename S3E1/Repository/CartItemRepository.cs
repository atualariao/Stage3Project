using Dapper;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;
using System.Data;

namespace S3E1.Repository
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly DataConnectionContext _connectionContext;

        public CartItemRepository(DataConnectionContext connectionContext) => _connectionContext = connectionContext;


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
            var query = "INSERT INTO CartItems (ItemID, ItemName, ItemPrice) VALUES (@ItemID, @ItemName, @ItemPrice)";

            var parameters = new DynamicParameters();
                parameters.Add("ItemID", itemEntity.ItemID, DbType.Guid);
                parameters.Add("ItemName", itemEntity.ItemName, DbType.String);
                parameters.Add("ItemPrice", itemEntity.ItemPrice, DbType.Double);

            using (var connection = _connectionContext.CreateConnection())
            {
                await connection.ExecuteAsync(query, parameters);
            }
        }
    }
}
