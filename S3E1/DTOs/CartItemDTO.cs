using S3E1.Enumerations;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace S3E1.DTOs
{
    public class CreateCartItemDTO
    {
        [Required]
        public string ItemName { get; set; } = null!;
        [Required]
        public double ItemPrice { get; set; }
        [Required]
        public Guid? CustomerID { get; set; }
    }
    public class CartItemDTO : CreateCartItemDTO
    {
        public Guid ItemID { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public OrderStatus OrderStatus { get; set; }
    }
}
