using Autofac;
using Autofac.Extensions.DependencyInjection;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.EntityFrameworkCore;
using S3E1;
using S3E1.Controllers.V1;
using S3E1.Data;
using S3E1.Middleware;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

//Autofac DIs
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacDependencyInjection());
    });

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = ApiVersion.Default;
    options.ReportApiVersions = true;

    //Controller Versions
    options.Conventions.Controller<CartItemsController>()
    .HasApiVersion(1, 0);
    options.Conventions.Controller<CheckOutController>()
    .HasApiVersion(1, 0);
    options.Conventions.Controller<OrderController>()
    .HasApiVersion(1, 0);
    options.Conventions.Controller<UserController>()
    .HasApiVersion(1, 0);
});

//Serilog Logger
var logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .CreateLogger();
builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

builder.Services.AddScoped<DbContext>(s =>
{
    var dbContextFactory = new MsSqlDbContextFactory(builder.Configuration.GetConnectionString("DefaultConnection"));
    return dbContextFactory.CreateDbContext();
});

////DB Context
//builder.Services.AddDbContext<AppDataContext>(contextOptions => contextOptions.UseSqlServer(
//    builder.Configuration.GetConnectionString("DefaultConnection")
//));

//Auth
//builder.Services.AddAuthentication();

//Swagger UI Auth
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stage 3 Exercise 1");
    });
}

app.UseHttpsRedirection();
app.UseUserAuth();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program
{
    //Expose the Program class for use in integration test project with WebAppFactory<T>
}

