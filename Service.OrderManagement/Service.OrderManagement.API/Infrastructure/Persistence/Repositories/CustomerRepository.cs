using Microsoft.EntityFrameworkCore;
using Service.OrderManagement.API.Core.Entities;
using Service.OrderManagement.API.Core.Repositories;

namespace Service.OrderManagement.API.Infrastructure.Persistence
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _dbContext;

        public CustomerRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetOrderCountForCustomerAsync(int customerId)
        {
            return await _dbContext.Orders
                .CountAsync(o => o.CustomerId == customerId);
        }

        public async Task<List<Order>> GetOrdersForCustomerAsync(int customerId)
        {
            return await _dbContext.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.OrderItems)
                .Include(o => o.Customer)
                .ToListAsync();
        }
    }
}
