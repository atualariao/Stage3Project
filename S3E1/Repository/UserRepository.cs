using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using S3E1.Entities;
using S3E1.IRepository;
using System.Data;

namespace S3E1.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;
        private readonly ILogger<UserRepository> _logger;
        private IDbConnection _connection;

        public UserRepository(DbContext dbContext, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<UserEntity> GetUserById(Guid id)
        {
            try
            {
                var query = "SELECT * FROM Users WHERE UserID = @id;" +
                                "SELECT * FROM Orders WHERE UserOrderId = @id";
                //+ "SELECT * FROM CartItems WHERE OrderEntityOrderID = @id";

                using (var connection = _dbContext.Database.GetDbConnection())
                using (var multi = await connection.QueryMultipleAsync(query, new { id }))
                {
                    var user = await multi.ReadSingleOrDefaultAsync<UserEntity>();
                    //var orders = await multi.ReadSingleOrDefaultAsync<OrderEntity>();
                    if (user != null) //&& orders != null
                        user.Orders = (await multi.ReadAsync<OrderEntity>()).ToList();
                    //orders.CartItemEntity = (await multi.ReadAsync<CartItemEntity>()).ToList();

                    _logger.LogInformation("User retrieved from database, Guid: {0}", user.UserID.ToString().ToUpper());
                    return user;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in retrieving user, Details: {0}", ex);
                throw;
            }
        }

        public async Task<UserEntity> CreateUser(UserEntity users)
        {
            try
            {
                var user = new UserEntity()
                {
                    UserID = Guid.NewGuid(),
                    Username = users.Username
                };
                _dbContext.Set<UserEntity>().Add(user);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("New User Created in the Database, Object: {0}", JsonConvert.SerializeObject(user).ToUpper());
                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error in Creating User Details: {0}", ex);
                throw;
            }
        }
    }
}
