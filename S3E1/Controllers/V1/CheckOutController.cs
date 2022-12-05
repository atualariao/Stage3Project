using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;

namespace S3E1.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/checkout")]
    [Produces("application/json")]
    [ApiController]
    public class CheckOutController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<CheckOutController> _logger;
        private readonly IMapper _mapper;

        public CheckOutController(ISender sender, ILogger<CheckOutController> logger, IMapper mapper)
        {
            _logger = logger;
            _sender = sender;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<OrderEntity>> Checkout(OrderDTO orders)
        {
            _logger.LogInformation("POST order checkout executing...");
            try
            {
                if (orders.CartItemEntity.IsNullOrEmpty())
                    return BadRequest("Your cart is empty.");
                return await _sender.Send(new CheckOutCommand(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Method Order Checkout Error Details: {0}", ex);
                throw;
            }

        }
    }
}
