using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.Data;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Enumerations;
using Swashbuckle.AspNetCore.Annotations;

namespace eCommerceWebAPI.Controllers.V1
{
    //[Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/checkout")]
    [Produces("application/json")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly AppDataContext _dbContext;
        private readonly ILogger<CheckOutController> _logger;
        private readonly ISender _sender;

        public CheckOutController(ISender sender, ILogger<CheckOutController> logger, AppDataContext dbContext)
        {
            _logger = logger;
            _sender = sender;
            _dbContext = dbContext;
        }

        [SwaggerOperation(
            Summary = "Updates order and cart item status to processed",
            Description = "Updates order and cart item status to processed")]
        [HttpPost]
        public async Task<ActionResult<Order>> Checkout([FromBody] CheckOutDTO orders)
        {
            _logger.LogInformation("POST order checkout executing...");

            var command = new CheckOutCommand
            {
                UserId = orders.UserPrimaryID
            };

            try
            {
                var result = await _sender.Send(command);

                return result == null ? BadRequest("Your cart is empty") : Ok("Checkout Complete");
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Method Order Checkout Error Details: {0}", ex);
                throw;
            }

        }
    }
}
