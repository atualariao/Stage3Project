using System.ComponentModel.DataAnnotations;

namespace S3E1.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid UserID { get; set; }
        [Required]
        public string? Username { get; set; }
        public List<OrderEntity>? Orders { get; set; } = new List<OrderEntity>();
    }
}
