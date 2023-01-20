using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceWebAPI.Entities
{
    public class Order
    {
        [Key]
        public Guid PrimaryID { get; set; }
        [ForeignKey(nameof(User))]
        public Guid UserPrimaryID { get; set; }
        public User User { get; set; } = null!;
        public double OrderTotalPrice { get; set; }
        public DateTime OrderCreatedDate { get; set; } = DateTime.Now;
        public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
        public List<CartItem>? CartItemEntity { get; set; } = new List<CartItem>();
    }
}
