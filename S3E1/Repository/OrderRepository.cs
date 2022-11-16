using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using S3E1.DTO;

namespace S3E1.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DataConnectionContext _connectionContext;
        private readonly AppDataContext _appDataContext;

        public OrderRepository(DataConnectionContext connectionContext, AppDataContext appDataContext)
        {
            _connectionContext = connectionContext;
            _appDataContext = appDataContext;
        }

        //public async Task<List<OrderEntity>> GerOrders()
        //{
        //    var query = "SELECT * FROM Orders;" +
        //                    "SELECT * FROM CartItems WHERE itemStatus LIKE 'Processed'";

        //    using (var connection = _connectionContext.CreateConnection())
        //    using(var multi = await connection.QueryMultipleAsync(query))
        //    {
        //        var orders = await multi.ReadSingleOrDefaultAsync<OrderEntity>();
        //        if(orders != null)
        //            orders.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

        //        return orders;
        //    }
        //}

        public async Task<List<Orders>> GerOrders()
        {
            var query = "SELECT * FROM Orders";

            using (var connection = _connectionContext.CreateConnection())
            {
                var cartItems = await connection.QueryAsync<Orders>(query);

                return cartItems.ToList();
            }
        }

        public async Task<OrderEntity> GetOrderById(Guid id)
        {
            var query = "SELECT * FROM Orders WHERE OrderID = @id;" +
                                "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

            using(var connection = _connectionContext.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var order =  await multi.ReadSingleOrDefaultAsync<OrderEntity>();
                if (order != null)
                    order.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

                return order;
            }
        }

public async Task<Orders> UpdateOrder(Orders orders)
        {
            var order = await _appDataContext.Orders.FindAsync(orders.OrderID);
            order.OrderID = orders.OrderID;

            await _appDataContext.SaveChangesAsync();

            return orders;
        }

        public async Task<OrderEntity> DeleteOrderById(Guid id)
        {
            var order = _appDataContext.Orders.Find(id);
            var cartitems = _appDataContext.CartItems.ToList();
            foreach (var item in cartitems)
            {
                item.ItemStatus = "Pending";
            }

            _appDataContext.Orders.Remove(order);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.Orders.ToListAsync();

            return order;
        }
    }
}
