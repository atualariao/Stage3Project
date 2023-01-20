using eCommerceWebAPI.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebAPI.Entities
{
    public class CartItem
    {
        [Key]
        public Guid ItemID { get; set; }
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; }
        public Guid? OrderPrimaryID { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public Guid? CustomerID { get; set; }
    }
}