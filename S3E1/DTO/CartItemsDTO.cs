namespace S3E1.DTO
{
    public class CartItemsDTO
    {
        public Guid ItemID { get; set; } = Guid.NewGuid();
        public string? ItemName { get; set; }
        public double ItemPrice { get; set; }
        public string ItemStatus { get; set; } = "Pending";
    }
}
