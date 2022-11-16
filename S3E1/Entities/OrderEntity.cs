using S3E1.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3E1.Entities
{
    public class OrderEntity
    {
        [Key]
        public Guid OrderID { get; set; }
        public Guid UserOrderId { get; set; }
        public UserEntity User { get; set; }
        public double OrderTotalPrice { get; set; }
        public DateTime OrderCreatedDate { get; set; } = DateTime.Now;
        public List<CartItemEntity>? CartItemEntity { get; set; }
    }
}
