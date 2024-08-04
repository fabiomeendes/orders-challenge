namespace Service.Order.API.Core.Entities
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
