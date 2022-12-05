using Microsoft.EntityFrameworkCore;
using S3E1.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegrationTest
{
    public class TestDbFactory
    {
        public AppDataContext CreateDbContext()
        {
            var builder = new DbContextOptionsBuilder<AppDataContext>();
            builder.UseSqlite("DataSource=file::memory:");

            return new AppDataContext(builder.Options);
;        }
    }
}
