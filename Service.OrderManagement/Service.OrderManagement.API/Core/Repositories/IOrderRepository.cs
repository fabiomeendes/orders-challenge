namespace Service.OrderManagement.API.Core.Repositories
{
    public interface IOrderRepository
    {
        Task<decimal> GetTotalValueAsync(int orderId);
    }
}
