using Dapper;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;

namespace S3E1.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataConnectionContext _connectionContext;

        public OrderRepository(DataConnectionContext connectionContext) => _connectionContext = connectionContext;

        public async Task<IEnumerable<OrderEntity>> GerOrders()
        {
            var query = "SELECT * FROM Orders";

            using (var connection = _connectionContext.CreateConnection())
            {
                var orders = await connection.QueryAsync<OrderEntity>(query);

                return orders.ToList();
            }
        }

        public async Task<OrderEntity> GetOrderById(Guid id)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @id";

            using (var connection = _connectionContext.CreateConnection())
            {
                var order = await connection.QuerySingleOrDefaultAsync<OrderEntity>(query, new { id });
                return order;
            }
        }
    }
}
