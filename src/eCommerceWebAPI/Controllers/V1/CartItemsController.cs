using Azure;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace eCommerceWebAPI.Controllers.V1
{
    //[Authorize]
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
        public async Task<ActionResult<List<CartItemDTO>>> Get()
        {
            _logger.LogInformation("GET all cart items executing...");
            try
            {
                var result = await _sender.Send(new GetItemsQuery());
                return result.Any() ? Ok(result) : NotFound("Item list is empty.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET Method Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Returns a specific item from the cart",
            Description = "Returns a specific item from the cart")]
        [HttpGet("{itemID}")]
        public async Task<ActionResult<CartItem>> Get([FromRoute] Guid itemID)
        {
            _logger.LogInformation("GET cart item by Guid executing...");
            try
            {
                var result = await _sender.Send(new GetItemByIdQuery(itemID));
                return result == null ? NotFound("Item does not exist.") : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET Cart By Item Guid Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Adds an item to the cart",
            Description = "Adds an item to the cart")]
        [HttpPost]
        public async Task<ActionResult<CartItem>> Post([FromBody] CreateCartItemDTO cartItems)
        {
            var user = Request.Headers["x-user-id"].FirstOrDefault();
            var parsedUserId = Guid.Parse(user);

            _logger.LogInformation("POST cart items executing...");
            try
            {
                await _sender.Send(new AddCartItemCommand(cartItems, parsedUserId));
                return cartItems == null ? BadRequest("An error occured when adding item.") : Ok($"Item {cartItems.ItemName} successfully added to order.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"POST Cart Item Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Updates an item from the cart",
            Description = "Updates an item from the cart")]
        [HttpPut]
        public async Task<ActionResult<CartItem>> Update([FromBody] CartItemDTO cartItems)
        {
            _logger.LogInformation("PUT/UPDATE cart item executing...");
            try
            {
                await _sender.Send(new UpdateCartitemCommand(cartItems));
                return cartItems == null ? BadRequest("An error occured when updating item.") : Ok($"Item {cartItems.ItemName} successfully updated.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"PUT/UPDATE Cart Item Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Deletes an item from the cart",
            Description = "Deletes an item from the cart")]
        [HttpDelete("{itemID}")]
        public async Task<ActionResult<CartItem>> Delete([FromRoute] Guid itemID)
        {
            _logger.LogInformation("DELETE cart item executing...");
            try
            {
                await _sender.Send(new DeleteCartItemCommand(itemID));
                return itemID == Guid.Empty ? NotFound("item was not found.") : Ok($"Item {itemID} successfully removed from order.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"DELETE Cart Item Error Details: {ex}");
                throw;
            }
        }
    }


}
