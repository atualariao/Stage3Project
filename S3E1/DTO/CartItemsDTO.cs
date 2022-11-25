namespace S3E1.DTO
{
    public class CartItemsDTO
    {
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemStatus { get; set; } = "Pending";
    }
}
