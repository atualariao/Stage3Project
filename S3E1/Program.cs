using S3E1.Data;
using Microsoft.EntityFrameworkCore;

using MediatR;
using S3E1.Entities;
using S3E1.Contracts;
using S3E1.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//MediatR
builder.Services.AddScoped<ICartItemRepository, CartItemRepository>();
builder.Services.AddMediatR(typeof(CartItemRepository).Assembly);

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddMediatR(typeof(UserRepository).Assembly);

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddMediatR(typeof(OrderRepository).Assembly);

//Dependency Injection
builder.Services.AddDbContext<AppDataContext>(contextOptions => contextOptions.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
//Connection
builder.Services.AddSingleton<DataConnectionContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
