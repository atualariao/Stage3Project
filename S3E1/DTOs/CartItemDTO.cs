namespace S3E1.DTOs
{
    public class CartItemDTO
    {
        public Guid ItemID { get; set; } = Guid.NewGuid();
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; }
    }
}
