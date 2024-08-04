namespace Service.Order.API.Core.Repositories
{
    public interface IOrderRepository
    {
        public Task AddOrderAsync(Entities.Order order);
    }
}
