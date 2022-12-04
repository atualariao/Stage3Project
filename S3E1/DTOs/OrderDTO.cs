using S3E1.Entities;

namespace S3E1.DTOs
{
    public class OrderDTO
    {
        public Guid OrderID { get; set; }
        public Guid UserOrderId { get; set; }
        public List<CartItemEntity>? CartItemEntity { get; set; } = new List<CartItemEntity>();
    }
}
