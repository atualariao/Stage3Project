using Dapper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using eCommerceWebAPI.Data;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using System.Data;

namespace eCommerceWebAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDataContext _dbContext;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(AppDataContext dbContext, ILogger<UserRepository> logger)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task<User> GetUserById(Guid id)
        {
            try
            {
                var query = "SELECT * FROM Users WHERE UserID = @id;" +
                                "SELECT * FROM Orders WHERE UserPrimaryID = @id";

                using (var connection = _dbContext.Database.GetDbConnection())
                using (var multi = await connection.QueryMultipleAsync(query, new { id }))
                {
                    var user = await multi.ReadSingleOrDefaultAsync<User>();
                    if (user != null)
                        user.Orders = (await multi.ReadAsync<Order>()).ToList();

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

        public async Task<User> CreateUser(User user)
        {
            try
            {
                _dbContext.Users.Add(user);
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
