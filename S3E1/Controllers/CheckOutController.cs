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

        public CheckOutController(ISender sender) => _sender = sender;

        [HttpPost]
        public async Task<ActionResult<OrderEntity>> Checkout(OrderEntity orders)
        {
            if (orders.CartItemEntity.IsNullOrEmpty())
                return BadRequest("Your cart is empty.");
            return await _sender.Send(new CheckOutCommand(orders));

        }
    }
}
