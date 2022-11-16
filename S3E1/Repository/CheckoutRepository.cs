﻿using Microsoft.EntityFrameworkCore;
using S3E1.Contracts;
using S3E1.Data;
using S3E1.DTO;
using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace S3E1.Repository
{
    public class CheckoutRepository : ICheckoutRepository
    {
        private readonly AppDataContext _context;

        public CheckoutRepository(AppDataContext context) => _context = context;

        public async Task<Orders> Checkout(Orders orders)
        {
            var cartItems = _context.CartItems.ToList();
            var TotalPrice = _context.CartItems.Where(item => item.ItemStatus == "Pending").Sum(item => item.ItemPrice);

            foreach (var item in cartItems)
            {
                if (item.ItemStatus == "Pending")
                {
                    item.ItemStatus = "Processed";
                }
            }

            var userOrder = new OrderEntity()
            {
                OrderID = Guid.NewGuid(),
                UserOrderId = orders.UserOrderId,
                OrderTotalPrice = TotalPrice,
                OrderCreatedDate = DateTime.Now,
                CartItemEntity = _context.CartItems.ToList()
            };
            _context.Orders.Add(userOrder);
            _context.SaveChanges();
            await _context.Orders.ToListAsync();
            return orders;

        }
    }
}
