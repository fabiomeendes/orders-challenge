using Service.OrderManagement.API.Core.Repositories;

namespace Service.OrderManagement.API.Application.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        public OrderService(IOrderRepository OrderRepository)
        {
            _orderRepository = OrderRepository;
        }
        public async Task<decimal> GetTotalValue(int orderId)
        {
            return await _orderRepository.GetTotalValueAsync(orderId);
        }
    }
}
