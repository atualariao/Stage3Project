using Microsoft.EntityFrameworkCore;
using eCommerceWebAPI.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest.Data
{
    public class TestDbFactory
    {
        public AppDataContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<AppDataContext>();
            builder.UseSqlite("DataSource=file::memory:");
            //builder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=eCommerceWebAPIDev;Trusted_Connection=True;");

            return new AppDataContext(builder.Options);
        }
    }
}
