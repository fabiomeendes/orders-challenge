using System.ComponentModel.DataAnnotations.Schema;

namespace Service.OrderManagement.API.Core.Entities
{
    [Table("orders")]
    public partial class Order
    {
        [Column("orderid")]
        public int OrderId { get; set; }
        [Column("code")]
        public string Code { get; set; }
        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("totalvalue")]
        public decimal TotalValue { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
    }
}
