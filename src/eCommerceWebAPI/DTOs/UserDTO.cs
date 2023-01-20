using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.DTOs
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
