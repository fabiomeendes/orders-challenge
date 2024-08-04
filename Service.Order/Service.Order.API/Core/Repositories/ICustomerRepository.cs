namespace Service.Order.API.Core.Repositories
{
    public interface ICustomerRepository
    {
        public Task<Entities.Customer?> GetCustomerAsync(int customerId);
    }
}
