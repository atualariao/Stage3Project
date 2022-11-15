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
            var TotalPrice = _context.CartItems.Sum(item => item.ItemPrice);
            var cartStatus = _context.CartItems.Where(status => status.ItemStatus == "Status").ToList();

            foreach (var item in cartStatus)
            {
                var userOrder = new OrderEntity()
                {
                    OrderID = Guid.NewGuid(),
                    UserOrderId = orderEntity.UserOrderId,
                    OrderTotalPrice = TotalPrice,
                    OrderCreatedDate = DateTime.Now,
                    CartItemEntity = _context.CartItems.ToList()
                };

                if (cartStatus != null)
                {
                    item.ItemStatus = "Processed";

                    _context.Orders.Add(userOrder);
                    _context.SaveChanges();
                    await _context.Orders.ToListAsync();
                }
            }
            return orderEntity;

        }
    }
}
