using S3E1.Entities;

namespace S3E1.DTO
{
    public class Orders
    {
        public Guid OrderID { get; set; }
        public Guid UserOrderId { get; set; }
        public double OrderTotalPrice { get; set; }
        public DateTime OrderCreatedDate { get; set; } = DateTime.Now;
        public List<CartItemEntity> CartItemEntity { get; set; } = null!;
    }
}
