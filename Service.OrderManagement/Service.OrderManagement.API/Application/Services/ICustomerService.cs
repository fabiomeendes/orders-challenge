using Service.OrderManagement.API.Application.ViewModels;

namespace Service.OrderManagement.API.Application.Services
{
    public interface ICustomerService
    {
        public Task<int> GetOrderCountForCustomer(int customerId);
        public Task<List<OrderViewModel>> GetOrdersForCustomer(int customerId);
    }
}
