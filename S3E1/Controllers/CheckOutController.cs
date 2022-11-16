using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using S3E1.Commands;
using S3E1.Data;
using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace S3E1.Controllers
{
    [Route("api/checkout")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ISender _sender;

        public CheckOutController(ISender sender, AppDataContext appDataContext) => _sender = sender;

        [HttpPost]
        public async Task<ActionResult<OrderEntity>> Checkout(OrderEntity orderEntity)
        {
            if (orderEntity.CartItemEntity.IsNullOrEmpty())
                {
                    return BadRequest("Your cart is empty.");
                } 
            else 
                {
                    return await _sender.Send(new CheckOutCommand(orderEntity));
                }
        }
    }
}
