using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace S3E1.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/cart-items")]
    [Produces("application/json")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private ISender _sender;
        private readonly ILogger<CartItemsController> _logger;

        public CartItemsController(ISender sender, ILogger<CartItemsController> logger)
        {
            _logger = logger;
            _sender = sender;
        }

        [SwaggerOperation(
            Summary = "Returns all the items from the cart",
            Description = "Returns all the items from the cart")]
        [HttpGet]
        public async Task<List<CartItemDTO>> Get()
        {
            _logger.LogInformation("GET all cart items executing...");
            try
            {
                return await _sender.Send(new GetItemsQuery());
            }
            catch (Exception ex)
            {
                _logger.LogError("GET Method Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Returns a specific item from the cart",
            Description = "Returns a specific item from the cart")]
        [HttpGet("{itemID}")]
        public async Task<CartItem> Get(Guid itemID)
        {
            _logger.LogInformation("GET cart item by Guid executing...");
            try
            {
                return await _sender.Send(new GetItemByIdQuery(itemID));
            }
            catch (Exception ex)
            {
                _logger.LogError("GET Cart By Item Guid Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Adds an item to the cart",
            Description = "Adds an item to the cart")]
        [HttpPost]
        public async Task<CartItem> Post([FromBody] CreateCartItemDTO cartItems)
        {
            _logger.LogInformation("POST cart items executing...");
            try
            {
                return await _sender.Send(new AddCartItemCommand(cartItems));
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Cart Item Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Updates an item from the cart",
            Description = "Updates an item from the cart")]
        [HttpPut]
        public async Task<CartItem> Update(CartItemDTO cartItems)
        {
            _logger.LogInformation("PUT/UPDATE cart item executing...");
            try
            {
                return await _sender.Send(new UpdateCartitemCommand(cartItems));
            }
            catch (Exception ex)
            {
                _logger.LogError("PUT/UPDATE Cart Item Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Deletes an item from the cart",
            Description = "Deletes an item from the cart")]
        [HttpDelete("{itemID}")]
        public async Task<CartItem> Delete(Guid itemID)
        {
            _logger.LogInformation("DELETE cart item executing...");
            try
            {
                return await _sender.Send(new DeleteCartItemCommand(itemID));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE Cart Item Error Details: {0}", ex);
                throw;
            }
        }
    }


}
