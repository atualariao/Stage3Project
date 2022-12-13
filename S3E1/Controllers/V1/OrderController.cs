using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace S3E1.Controllers.V1
{
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
        public async Task<List<OrderDTO>> Get()
        {
            _logger.LogInformation("GET all orders executing");
            try
            {
                return await _sender.Send(new GetOrdersQuery());
            }
            catch (Exception ex)
            {
                _logger.LogError("GET All Method Order Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Returns a specific order",
            Description = "Returns a specific order")]
        [HttpGet("{PrimaryID}")]
        public async Task<Order> GetById(Guid PrimaryID)
        {
            _logger.LogInformation("GET order by Guid executing");
            try
            {
                return await _sender.Send(new GetOrdersByIdQuery(PrimaryID));
            }
            catch (Exception ex)
            {
                _logger.LogError("GET by Guid Method Order Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Updates items in an order",
            Description = "Updates items in an order")]
        [HttpPut]
        public async Task<Order> UpdateOrder(Order orders)
        {
            _logger.LogInformation("PUT/UPDATE order executing");
            try
            {
                return await _sender.Send(new UpdateOrderCommand(orders));
            }
            catch (Exception ex)
            {
                _logger.LogError("PUT/UPDATE Method Order Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Deletes a specific order",
            Description = "Deletes a specific order")]
        [HttpDelete("{PrimaryID}")]
        public async Task<Order> DeleteOrder(Guid PrimaryID)
        {
            _logger.LogInformation("DELETE order by Guid executing");
            try
            {
                return await _sender.Send(new DeleteOrderCommand(PrimaryID));
            }
            catch (Exception ex) 
            { 
                _logger.LogError("DELETE by Guid Method Order Error Details: {0}", ex); 
                throw; 
            }
        }
    }
}
