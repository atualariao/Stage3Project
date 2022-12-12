using S3E1.Entities;
using S3E1.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3E1.Entities
{
    public class Order
    {
        [Key]
        public Guid PrimaryID { get; set; }
        [ForeignKey(nameof(User))]
        [Required]
        public Guid UserPrimaryID { get; set; }
        public User User { get; set; } = null!;
        public double OrderTotalPrice { get; set; }
        public DateTime OrderCreatedDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public List<CartItem>? CartItemEntity { get; set; } = new List<CartItem>();
    }
}
