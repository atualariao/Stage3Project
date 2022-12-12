//using Microsoft.EntityFrameworkCore;
//using S3E1.Data;
//using System.Data.Entity.Infrastructure;

//namespace S3E1.Data
//{

//    public class MsSqlDbContextFactory : IDbContextFactory
//    {
//        private readonly string _connectionString;

//        public MsSqlDbContextFactory(string connectionString)
//        {
//            _connectionString = connectionString;
//        }

//        public AppDataContext CreateDbContext()
//        {
//            DbContextOptions options = new DbContextOptionsBuilder().UseSqlServer(_connectionString).Options;

//            return new AppDataContext(options);
//        }
//    }

//    public interface IDbContextFactory
//    {
//        AppDataContext CreateDbContext();
//    }
//}