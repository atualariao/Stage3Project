using Dapper;
using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
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

        public async Task<UserEntity> GetUserById(Guid id)
        {
            var query = "SELECT * FROM Users WHERE UserID = @id";

            using (var connection = _connectionContext.CreateConnection())
            {
                var user = await connection.QuerySingleOrDefaultAsync<UserEntity>(query, new { id });

                return user;
            }
        }

        public async Task<UserEntity> CreateUser(UserEntity userEntity)
        {
            var user = new UserEntity()
            {
                 UserID = Guid.NewGuid(),
                 Username = userEntity.Username,
            };
            _appDataContext.Users.Add(user);
            await _appDataContext.SaveChangesAsync();
            await _appDataContext.Users.ToListAsync();

            return userEntity;


            //DAPPER
            //var query = "INSERT INTO Users (UserID, Username) VALUES (@UserID, @Username)";

            //var parameters = new DynamicParameters();
            //    parameters.Add("UserID", userEntity.UserID, DbType.Guid);
            //    parameters.Add("Username", userEntity.Username, DbType.String);

            //using (var connection = _connectionContext.CreateConnection())
            //{
            //    await connection.ExecuteAsync(query, parameters);

            //    return userEntity;
            //}
        }
    }
}
