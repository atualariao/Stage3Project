using System.ComponentModel.DataAnnotations;

namespace S3E1.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; } = null!;
    }

    public class UserDTO : CreateUserDTO
    {
        public Guid UserID { get; set; }
    }

}
