using Dapper;
using IntegrationTest.Handlers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S3E1.Data;
using System.Net.Http.Headers;
using System.Text;

namespace IntegrationTest.Data
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
                        services.RemoveAll(typeof(AppDataContext));
                        services.AddScoped(s =>
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