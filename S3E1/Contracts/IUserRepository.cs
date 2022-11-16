using S3E1.DTO;
using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserById(Guid id);
        public Task<Users> CreateUser(Users users);
    }
}
