using System.ComponentModel.DataAnnotations;

namespace S3E1.Entities
{
    public class UserEntity
    {
        [Key]
        public Guid UserID { get; set; } = Guid.NewGuid();
        [Required]
        public string? Username { get; set; }
    }
}
