using S3E1.Entities;

namespace S3E1.IRepository
{
    public interface IUserRepository
    {
        public Task<UserEntity> GetUserById(Guid id);
        public Task<UserEntity> CreateUser(UserEntity users);
    }
}
