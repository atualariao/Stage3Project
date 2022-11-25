using S3E1.Data;
using Microsoft.EntityFrameworkCore;

using MediatR;
using S3E1.Entities;
using S3E1.Contracts;
using S3E1.Repository;
using System.Security.Claims;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
//using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Swashbuckle.AspNetCore.Filters;
using S3E1.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//MediatR DIs
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddMediatR(typeof(CartItemRepository).Assembly);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddMediatR(typeof(UserRepository).Assembly);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddMediatR(typeof(OrderRepository).Assembly);

builder.Services.AddScoped<ICheckoutRepository, CheckoutRepository>();
builder.Services.AddMediatR(typeof(CheckoutRepository).Assembly);

//DB Context
builder.Services.AddDbContext<AppDataContext>(contextOptions => contextOptions.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));

//In Memory
//builder.Services.AddDbContext<AppDataContext>(contextOptions =>
//{
//    contextOptions.UseInMemoryDatabase("TestDB");
//});

//Connection
builder.Services.AddScoped<DataConnectionContext>();

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