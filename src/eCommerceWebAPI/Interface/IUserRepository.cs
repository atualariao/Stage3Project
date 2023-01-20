using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Interface
{
    public interface IUserRepository
    {
        public Task<User> GetUserById(Guid id);
        public Task<User> CreateUser(User users);
    }
}
