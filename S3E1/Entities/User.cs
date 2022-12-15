using System.ComponentModel.DataAnnotations;

namespace S3E1.Entities
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        [Required]
        public string? Username { get; set; }
        public List<Order>? Orders { get; set; } = new List<Order>();
    }
}
