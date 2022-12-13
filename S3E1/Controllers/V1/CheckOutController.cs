using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using S3E1.Commands;
using S3E1.Data;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Enumerations;

namespace S3E1.Controllers.V1
{
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

        [HttpPost]
        public async Task<ActionResult<Order>> Checkout(CheckOutDTO orders)
        {
            _logger.LogInformation("POST order checkout executing...");
            var order = _dbContext
                .Orders
                .Where(user => user.UserPrimaryID == orders.UserPrimaryID && user.OrderStatus == OrderStatus.Pending)
                .ToList();
            try
            {
                await _sender.Send(new CheckOutCommand(orders));
                return order.Count == 0 ? BadRequest("Your cart is empty") : Ok("Checkout Complete");
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Method Order Checkout Error Details: {0}", ex);
                throw;
            }

        }
    }
}
