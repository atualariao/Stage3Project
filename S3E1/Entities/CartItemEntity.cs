using System.ComponentModel.DataAnnotations;

namespace S3E1.Entities
{
    public class CartItemEntity
    {
        [Key]
        public Guid ItemID { get; set; } = Guid.NewGuid();

        [Required]
        [StringLength(50)]
        public string? ItemName { get; set; }

        [Required]
        public double ItemPrice { get; set; }
    }
}
