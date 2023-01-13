using S3E1.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3E1.Entities
{
    public class CartItem
    {
        [Key]
        public Guid ItemID { get; set; }
        [Required]
        public string? ItemName { get; set; }
        [Required]
        public double ItemPrice { get; set; }
        public Guid? OrderPrimaryID { get; set; }
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        //public Guid? CustomerID { get; set; }
    }
}