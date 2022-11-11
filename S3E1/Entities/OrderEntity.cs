using S3E1.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace S3E1.Entities
{
    public class OrderEntity
    {
        [Key]
        public Guid OrderID { get; set; } = Guid.NewGuid();

        [ForeignKey("UserModel")]
        public Guid UserRefID { get; set; }
        public UserEntity UserModel { get; set; }

        //public double OrderTotalPrice { get; set; }

        public DateTime OrderCreatedDate { get; set; } = DateTime.Now;

        public List<CartItemEntity> CartItems { get; set; } = new List<CartItemEntity>();
    }
}
