using System.ComponentModel.DataAnnotations;

namespace eCommerceWebAPI.Entities
{
    public class User
    {
        [Key]
        public Guid UserID { get; set; }
        public string? Username { get; set; }
        public List<Order>? Orders { get; set; } = new List<Order>();
    }
}
