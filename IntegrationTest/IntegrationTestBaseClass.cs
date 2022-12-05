using AutoMapper;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S3E1.Data;

namespace IntegrationTest
{
    public class IntegrationTestBaseClass
    {
        protected readonly HttpClient _httpClient;

        public IntegrationTestBaseClass()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(DbContextOptions<AppDataContext>));
                        services.AddScoped<DbContext>(s =>
                        {
                            var dbContextFactory = new TestDbFactory();
                            var dbContext = dbContextFactory.CreateDbContext();
                            dbContext.Database.EnsureCreated();

                            return dbContext;
                        });

                        SqlMapper.AddTypeHandler(new MySqlGuidTypeHandler());
                        SqlMapper.RemoveTypeMap(typeof(Guid));
                        SqlMapper.RemoveTypeMap(typeof(Guid?));
                        SqlMapper.AddTypeHandler(new GuidHandler());
                    });
                });

            _httpClient = appFactory.CreateClient();
        }
    }
}