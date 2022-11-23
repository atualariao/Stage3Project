using S3E1.Entities;
using System.ComponentModel.DataAnnotations;

namespace S3E1.DTO
{
    public class UsersDTO
    {
        public Guid UserID { get; set; }
        [Required]
        public string? Username { get; set; }
        public List<OrdersDTO>? Orders { get; set; }
    }
}
