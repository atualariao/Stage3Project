using Microsoft.Data.SqlClient;
using System.Data;

namespace S3E1.Data
{
    public class DataConnectionContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DataConnectionContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionString);

    }
}
