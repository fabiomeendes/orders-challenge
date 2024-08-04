namespace Service.Order.API.Core.Entities
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public Order Order { get; set; }
    }
}
