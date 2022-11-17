using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.DTO;
using S3E1.Entities;
using System.Data;

namespace S3E1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataConnectionContext _connectionContext;
        private readonly AppDataContext _appDataContext;

        public UserRepository(DataConnectionContext connectionContext, AppDataContext appDataContext)
        {
            _connectionContext = connectionContext;
            _appDataContext = appDataContext;
        }

        public async Task<Users> GetUserById(Guid id)
        {
            var query = "SELECT * FROM Users WHERE UserID = @id;" +
                                "SELECT * FROM Orders WHERE UserOrderId = @id";
            //+ "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

            using (var connection = _connectionContext.CreateConnection())
            using (var multi = await connection.QueryMultipleAsync(query, new { id }))
            {
                var user = await multi.ReadSingleOrDefaultAsync<Users>();
                //var orders = await multi.ReadSingleOrDefaultAsync<OrderEntity>();
                if (user != null) //&& orders != null
                    user.Orders = (await multi.ReadAsync<Orders>()).ToList();
                    //orders.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

                return user;
            }
        }

        public async Task<Users> CreateUser(Users users)
        {
            var user = new UserEntity()
            {
                 UserID = Guid.NewGuid(),
                 Username = users.Username
            };
            _appDataContext.Users.Add(user);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.Users.ToListAsync();

            return users;
        }
    }
}
