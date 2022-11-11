using Microsoft.EntityFrameworkCore;
using S3E1.Entities;

namespace S3E1.Data
{
    public class AppDataContext : DbContext
    {
        
        public AppDataContext(DbContextOptions<AppDataContext> contextOptions) : base(contextOptions) { }

        public DbSet<CartItemEntity> CartItems { get; set; }
      
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<UserEntity> Users{ get; set; }
        
    }
}
