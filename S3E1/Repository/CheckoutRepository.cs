using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace S3E1.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDataContext _context;

        public CheckoutRepository(AppDataContext context) => _context = context;

        public async Task<OrderEntity> Checkout(OrderEntity orderEntity)
        {
            var cartItems = _context.CartItems.ToList();

            var userOrder = new OrderEntity()
            {
                OrderID = Guid.NewGuid(),
                UserOrderId = orderEntity.UserOrderId,
                OrderCreatedDate = DateTime.Now,
                CartItems = cartItems,
            };
           
            _context.Orders.Add(userOrder);
            _context.SaveChanges();
            await _context.Orders.ToListAsync();

            return userOrder;
        }
    }
}
