using Service.OrderManagement.API.Core.Entities;

namespace Service.OrderManagement.API.Core.Repositories
{
    public interface ICustomerRepository
    {
        Task<int> GetOrderCountForCustomerAsync(int customerId);
        Task<List<Order>> GetOrdersForCustomerAsync(int customerId);
    }
}
