namespace S3E1.IRepository
{
    public interface IAdminRepository
    {
        bool ValidateCredentials(string username, string password);
    }
}
