using Dapper;
using IntegrationTest.Handlers;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S3E1.Data;
using System.Net.Http.Headers;

namespace IntegrationTest.Data
{
    public class IntegrationTestBaseClass
    {
        protected readonly HttpClient _httpClient;
        public Guid DefaultUserId { get; set; } = new Guid("ED9C2025-018D-4135-BEC9-BC17AEA8AD47");

        public IntegrationTestBaseClass()
        {
            var appFactory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.Configure<TestAuthHandlerOptions>(options => options.UserID = DefaultUserId);
                        services.Configure<TestAuthHandlerOptions>(options => options.Username = "Test User");
                        services.AddAuthentication(TestAuthHandler.AutheticationScheme)
                        .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(TestAuthHandler.AutheticationScheme, 
                        options => { });

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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Test");
        }
    }
}