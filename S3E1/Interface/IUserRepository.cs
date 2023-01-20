using S3E1.Entities;

namespace S3E1.Interface
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(Guid id);
        public Task<User> CreateUser(User users);
    }
}
