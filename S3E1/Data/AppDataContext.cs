using Microsoft.EntityFrameworkCore;
using S3E1.Entities;

namespace S3E1.Data
{
    public class AppDataContext : DbContext
    {
        

        public DbSet<CartItemEntity> CartItems { get; set; }
      
        public DbSet<OrderEntity> Orders { get; set; }

        public DbSet<UserEntity> Users{ get; set; }
        public AppDataContext(DbContextOptions<AppDataContext> contextOptions) : base(contextOptions) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasMany(order => order.Orders)
                .WithOne(user => user.User)
                .HasForeignKey(fkey => fkey.UserOrderId);

            modelBuilder.Entity<OrderEntity>()
                .HasOne(user => user.User)
                .WithMany(order => order.Orders)
                .HasForeignKey(fkey => fkey.UserOrderId);
        }


    }
}
