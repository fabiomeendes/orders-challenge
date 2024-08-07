using Microsoft.EntityFrameworkCore;
using Service.OrderManagement.API.Core.Repositories;

namespace Service.OrderManagement.API.Infrastructure.Persistence
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _dbContext;

        public OrderRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<decimal> GetTotalValueAsync(int orderId)
        {
            var order = await _dbContext.Orders
                .Where(o => o.OrderId == orderId)
                .FirstOrDefaultAsync();

            if (order == null)
            {
                throw new KeyNotFoundException("Order not found.");
            }

            return order.TotalValue;
        }
    }
}
