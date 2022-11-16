using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using S3E1.Commands;
using S3E1.Data;
using S3E1.DTO;
using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace S3E1.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly AppDataContext _appDataContext;

        public CheckOutController(ISender sender, AppDataContext appDataContext)
        {
            _sender = sender;
            _appDataContext = appDataContext;
        }

        [HttpPost]
        public async Task<ActionResult<Orders>> Checkout(Orders orders)
        {
            var cartItems = _appDataContext.CartItems.ToList();
            var orderItems = _appDataContext.Orders.ToList();

            foreach (var order in orderItems)
            {
                foreach (var item in cartItems)
                {
                    if (order.CartItemEntity.IsNullOrEmpty() && item.ItemStatus != "Pending")
                        return BadRequest("Your cart is empty.");
                }
            }
            return await _sender.Send(new CheckOutCommand(orders));

        }
    }
}
