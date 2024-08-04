using Dapper;
using Service.Order.API.Core.Repositories;
using System.Data;

namespace Service.Order.API.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IDbConnection _dbConnection;

        public OrderRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task AddOrderAsync(Core.Entities.Order order)
        {
            if (_dbConnection.State == ConnectionState.Closed)
            {
                _dbConnection.Open();
            }

            using (var transaction = _dbConnection.BeginTransaction())
            {
                try
                {
                    var orderSql = "INSERT INTO orders (Code, CustomerId, TotalValue) VALUES (@Code, @CustomerId, @TotalValue) RETURNING OrderId";
                    var orderId = await _dbConnection.ExecuteScalarAsync<int>(orderSql, new { order.Code, order.CustomerId, order.TotalValue }, transaction);

                    var orderItemSql = "INSERT INTO orderitems (OrderId, Product, Quantity, Price) VALUES (@OrderId, @Product, @Quantity, @Price)";
                    foreach (var item in order.OrderItems)
                    {
                        await _dbConnection.ExecuteAsync(orderItemSql, new { OrderId = orderId, item.Product, item.Quantity, item.Price }, transaction);
                    }

                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
                finally
                {
                    _dbConnection.Close();
                }
            }
        }
    }
}
