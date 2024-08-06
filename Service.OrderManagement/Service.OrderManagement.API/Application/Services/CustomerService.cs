using Service.OrderManagement.API.Application.ViewModels;

namespace Service.OrderManagement.API.Application.Services
{
    public class CustomerService : ICustomerService
    {
        public Task<int> GetOrderCountForCustomer(int customerId)
        {
            throw new NotImplementedException();
        }

        public Task<List<OrderViewModel>> GetOrdersForCustomer(int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
