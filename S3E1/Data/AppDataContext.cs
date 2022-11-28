using Microsoft.EntityFrameworkCore;
using S3E1.Entities;

namespace S3E1.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions contextOptions) : base(contextOptions) { }
        public DbSet<CartItemEntity> CartItems { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<UserEntity> Users{ get; set; }


        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<UserEntity>()
                .HasMany(order => order.Orders)
                .WithOne(user => user.User)
                .HasForeignKey(fkey => fkey.UserOrderId);

            modelbuilder.Entity<OrderEntity>()
                .HasOne(user => user.User)
                .WithMany(order => order.Orders)
                .HasForeignKey(fkey => fkey.UserOrderId);
        }


    }
}
