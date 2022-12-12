using Microsoft.EntityFrameworkCore;
using S3E1.Entities;
using S3E1.Enumerations;

namespace S3E1.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions contextOptions) : base(contextOptions) { }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users{ get; set; }

        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            modelbuilder.Entity<Order>()
                .Property(status => status.OrderStatus)
                .HasConversion(
                    s => s.ToString(),
                    s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
            modelbuilder.Entity<CartItem>()
                .Property(status => status.OrderStatus)
                .HasConversion(
                s => s.ToString(),
                s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
        }


    }
}
