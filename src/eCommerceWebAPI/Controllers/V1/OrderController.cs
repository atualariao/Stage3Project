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
    [Route("api/v{version:apiVersion}/orders")]
    [Produces("application/json")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<OrderController> _logger;

        public OrderController(ISender sender, ILogger<OrderController> logger)
        {
            _logger = logger;
            _sender = sender;
        }

        [SwaggerOperation(
            Summary = "Returns all orders",
            Description = "Returns all orders")]
        [HttpGet]
        public async Task<ActionResult<List<OrderDTO>>> Get()
        {
            _logger.LogInformation("GET all orders executing");
            try
            {
                var result = await _sender.Send(new GetOrdersQuery());
                return result.Any() ? Ok(result) : NotFound("Order list is empty.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET All Method Order Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Returns a specific order",
            Description = "Returns a specific order")]
        [HttpGet("{PrimaryID}")]
        public async Task<ActionResult<Order>> GetById([FromRoute] Guid PrimaryID)
        {
            _logger.LogInformation("GET order by Guid executing");
            try
            {
                var result = await _sender.Send(new GetOrdersByIdQuery(PrimaryID));
                return result == null ? NotFound("Order does not exist.") : Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError($"GET by Guid Method Order Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Updates items in an order",
            Description = "Updates items in an order")]
        [HttpPut]
        public async Task<ActionResult<Order>> UpdateOrder([FromBody] OrderDTO orderDTO)
        {
            _logger.LogInformation("PUT/UPDATE order executing");
            try
            {
                var result = await _sender.Send(new UpdateOrderCommand(orderDTO));
                return result == null ? BadRequest("An error occured when updating order.") : Ok($"Order {result.PrimaryID} updated successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError($"PUT/UPDATE Method Order Error Details: {ex}");
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Deletes a specific order",
            Description = "Deletes a specific order")]
        [HttpDelete("{PrimaryID}")]
        public async Task<ActionResult<Order>> DeleteOrder([FromRoute] Guid PrimaryID)
        {
            _logger.LogInformation("DELETE order by Guid executing");
            try
            {
                var result = await _sender.Send(new DeleteOrderCommand(PrimaryID));
                return result == null ? NotFound("Order does not exist.") : Ok($"Order {result.UserPrimaryID} deleted successfully.");
            }
            catch (Exception ex) 
            { 
                _logger.LogError($"DELETE by Guid Method Order Error Details: {ex}"); 
                throw; 
            }
        }
    }
}
