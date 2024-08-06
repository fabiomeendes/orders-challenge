namespace Service.OrderManagement.API.Application.Services
{
    public interface IOrderService
    {
        public Task<decimal> GetTotalValue(int orderId);
    }
}
