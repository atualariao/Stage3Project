namespace eCommerceWebAPI.Interface
{
    public interface IAdminRepository
    {
        bool ValidateCredentials(string username, string password);
    }
}
