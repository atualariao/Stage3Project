using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using S3E1.Commands;
using S3E1.Entities;

namespace S3E1.Controllers.V1
{
    [Route("api/checkout")]
    [Produces("application/json")]
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
