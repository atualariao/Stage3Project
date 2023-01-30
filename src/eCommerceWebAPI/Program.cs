using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using eCommerceWebAPI.Data;
using eCommerceWebAPI.Middleware;
using eCommerceWebAPI.Extensions;
using Serilog;
using Microsoft.AspNetCore.HttpOverrides;

//Environment Variable Strings
string msSQLConnectionString = Environment.GetEnvironmentVariable("MSSQL_CONNECTION_STRING") ?? "";

//Builder Services
WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

//Add services to the container.
//Cors
builder.Services.ConfigureCors();
//IIS Integration
builder.Services.ConfigureIISIntegration();
//AutoMapper
builder.Services.ConfigureAutoMapper();
//API Versioning
builder.Services.ConfigureSwaggerVersioning();
builder.Services.ConfigureSwaggerVersioningExplorer();
//Auth
//builder.Services.AddAuthentication();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.ConfigureSwaggerDocumentation();
//Add AuthScheme
builder.Services.ConfigureBasicAuth();
//AppDataContext DI
builder.Services.AddDbContextFactory<AppDataContext>(options =>
           options.UseSqlServer(msSQLConnectionString),
           ServiceLifetime.Scoped);

//Serilog Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

//Autofac DIs
builder.Host.ConfigureAutofac();

//Add Controllers
builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger( c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; });
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        // build a swagger endpoint for each discovered API version
        foreach (ApiVersionDescription description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    description.ApiVersion.ToString()
                );
        }
    });
} else
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

//Use cors
app.UseCors("CorsPolicy");
//Use Middleware
app.UseUserAuth();

//Use Authentication
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    //Expose the Program class for use in integration test project with WebAppFactory<T>
}

