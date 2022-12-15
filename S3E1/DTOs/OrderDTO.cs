using S3E1.Enumerations;
using System.Text.Json.Serialization;

namespace S3E1.DTOs
{
    public class CheckOutDTO
    {
        public Guid UserPrimaryID { get; set; }
    }

    public class OrderDTO : CheckOutDTO
    {
        public Guid PrimaryID { get; set; }
        public double OrderTotalPrice { get; set; }
        public DateTime OrderCreatedDate { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
        public List<CartItemDTO>? CartItemEntity { get; set; } = new List<CartItemDTO>();
    }
}