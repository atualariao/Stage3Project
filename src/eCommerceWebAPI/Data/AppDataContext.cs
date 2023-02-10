using Microsoft.EntityFrameworkCore;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Enumerations;

namespace eCommerceWebAPI.Data
{
    public class AppDataContext : DbContext
    {
        public AppDataContext(DbContextOptions contextOptions) : base(contextOptions) { }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users{ get; set; }

        //Parsing OrderStatus enum value to string
        //protected override void OnModelCreating(ModelBuilder modelbuilder)
        //{
        //    modelbuilder.Entity<Order>()
        //        .Property(status => status.OrderStatus)
        //        .HasConversion(
        //            s => s.ToString(),
        //            s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
        //    modelbuilder.Entity<CartItem>()
        //        .Property(status => status.OrderStatus)
        //        .HasConversion(
        //        s => s.ToString(),
        //        s => (OrderStatus)Enum.Parse(typeof(OrderStatus), s));
        //}


    }
}
