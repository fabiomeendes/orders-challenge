using Dapper;
using Service.Order.API.Core.Entities;
using Service.Order.API.Core.Repositories;
using System.Data;
using System.Transactions;

namespace Service.Order.API.Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IDbConnection _dbConnection;

        public CustomerRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<Customer?> GetCustomerAsync(int customerId)
        {
            try
            {
                var sql = "SELECT CustomerId, Name FROM Customers WHERE CustomerId = @CustomerId";
                var customer = await _dbConnection.QueryFirstOrDefaultAsync<Customer>(sql, new { CustomerId = customerId });
                return customer;
            }
            catch
            {
                throw;
            }
        }
    }
}
