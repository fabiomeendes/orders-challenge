using Service.OrderManagement.API.Application.ViewModels;
using Service.OrderManagement.API.Core.Repositories;

namespace Service.OrderManagement.API.Application.Services
{
    public class CustomerService : ICustomerService
    {
        private ICustomerRepository _customerRepository;
        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<int> GetOrderCountForCustomer(int customerId)
        {
            return await _customerRepository.GetOrderCountForCustomerAsync(customerId);
        }

        public async Task<List<OrderViewModel>> GetOrdersForCustomer(int customerId)
        {
            var orders = await _customerRepository.GetOrdersForCustomerAsync(customerId);

            var viewModels = orders.Select(o => new OrderViewModel(o)).ToList();

            return viewModels;
        }
    }
}
