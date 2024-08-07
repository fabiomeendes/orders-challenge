using System.ComponentModel.DataAnnotations.Schema;

namespace Service.OrderManagement.API.Core.Entities
{
    [Table("customers")]
    public partial class Customer
    {
        public Customer()
        {
            Orders = new HashSet<Order>();
        }

        [Column("customerid")]
        public int CustomerId { get; set; }
        [Column("name")]
        public string Name { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
