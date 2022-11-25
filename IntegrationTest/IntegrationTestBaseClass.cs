using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using S3E1.Data;

namespace IntegrationTest
{
    public class IntegrationTestBaseClass : IClassFixture<WebApplicationFactory<Program>>
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
                        services.AddDbContext<AppDataContext>();
                    });
                });
            _httpClient = appFactory.CreateClient();
        }
    }
}
