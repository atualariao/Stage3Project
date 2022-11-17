using S3E1.Entities;
using System.ComponentModel.DataAnnotations;

namespace S3E1.DTO
{
    public class Users
    {
        public Guid UserID { get; set; }
        [Required]
        public string? Username { get; set; }
        public List<Orders>? Orders { get; set; }
    }
}
