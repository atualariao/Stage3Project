using S3E1.Entities;
using System.ComponentModel.DataAnnotations;

namespace S3E1.DTOs
{
    public class OrderDTO
    {
        public Guid OrderID { get; set; } = Guid.NewGuid();
        public Guid UserOrderId { get; set; }
        public List<CartItemEntity>? CartItemEntity { get; set; } = new List<CartItemEntity>();
    }
}
