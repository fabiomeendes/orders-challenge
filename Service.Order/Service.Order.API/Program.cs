using Npgsql;
using Service.Order.API.Consumers;
using Service.Order.API.Core.Repositories;
using Service.Order.API.Infrastructure.Persistence;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHostedService<OrderCreatedConsumer>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<ICustomerRepository, CustomerRepository>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IDbConnection>(sp => new NpgsqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

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
