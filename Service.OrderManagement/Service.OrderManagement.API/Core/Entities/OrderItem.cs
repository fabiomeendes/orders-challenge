using System.ComponentModel.DataAnnotations.Schema;

namespace Service.OrderManagement.API.Core.Entities
{
    [Table("orderitems")]
    public partial class OrderItem
    {
        [Column("orderitemid")]
        public int OrderItemId { get; set; }
        [Column("orderid")]
        public int OrderId { get; set; }
        [Column("product")]
        public string Product { get; set; }
        [Column("quantity")]
        public int Quantity { get; set; }
        [Column("price")]
        public decimal Price { get; set; }

        public virtual Order Order { get; set; }
    }
}
