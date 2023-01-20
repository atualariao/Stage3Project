namespace S3E1.Interface
{
    public interface IAdminRepository
    {
        bool ValidateCredentials(string username, string password);
    }
}
