using S3E1.Entities;

namespace S3E1.DTO
{
    public class OrdersDTO
    {
        public Guid OrderID { get; set; }
        public Guid UserOrderId { get; set; }
        public double OrderTotalPrice { get; set; }
        public DateTime OrderCreatedDate { get; set; } = DateTime.Now;
    }
}
