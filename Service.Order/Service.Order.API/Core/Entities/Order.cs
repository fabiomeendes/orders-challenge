namespace Service.Order.API.Core.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalValue { get; set; }
        public string Code { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public Customer Customer { get; set; }
    }
}
