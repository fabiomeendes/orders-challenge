using Service.OrderManagement.API.Core.Entities;

namespace Service.OrderManagement.API.Application.ViewModels
{
    public class OrderViewModel
    {
        public OrderViewModel(Order order)
        {
            OrderId = order.OrderId;
            OrderCode = order.Code;
            CustomerId = order.Customer.CustomerId;
            CustomerName = order.Customer.Name;
            TotalValue = order.TotalValue;
            OrderItems = order.OrderItems.Select(o => new OrderItemViewModel(o)).ToList();
        }

        public int OrderId { get; set; }
        public string OrderCode { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public decimal TotalValue { get; set; }
        public List<OrderItemViewModel> OrderItems { get; set; }
        public OrderViewModel() { }
    }

    public class OrderItemViewModel
    {
        public OrderItemViewModel(OrderItem orderItem)
        {
            OrderItemId = orderItem.OrderItemId;
            Product = orderItem.Product;
            Price = orderItem.Price;
            Quantity = orderItem.Quantity;
        }
        public int OrderItemId { get; set; }
        public string Product { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public OrderItemViewModel() { }
    }
}
