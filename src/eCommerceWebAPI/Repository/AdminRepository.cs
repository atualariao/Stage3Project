using eCommerceWebAPI.Data;
using eCommerceWebAPI.Interface;

namespace eCommerceWebAPI.Repository
{
    public class AdminRepository : IAdminRepository
    {
        private readonly AppDataContext _appDataContext;

        public AdminRepository(AppDataContext appDataContext) => _appDataContext = appDataContext;

        public bool ValidateCredentials(string username, string password)
        {
            var userList = _appDataContext.Users.ToList();
            var userAdmin = userList.FirstOrDefault(x => x.Username == username && x.UserID.ToString() == password);

            return username.Equals(userAdmin.Username) && password.Equals(userAdmin.UserID.ToString());
        }
    }
}
