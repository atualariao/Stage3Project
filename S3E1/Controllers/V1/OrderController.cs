using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Queries;

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

        [HttpGet]
        public async Task<List<OrderEntity>> Get()
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

        [HttpGet("{id}")]
        public async Task<OrderEntity> GetById(Guid id)
        {
            _logger.LogInformation("GET order by Guid executing");
            try
            {
                return await _sender.Send(new GetOrdersByIdQuery(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("GET by Guid Method Order Error Details: {0}", ex);
                throw;
            }
        }

        [HttpPut]
        public async Task<OrderEntity> UpdateOrder(OrderEntity orders)
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

        [HttpDelete("{id}")]
        public async Task<OrderEntity> DeleteOrder(Guid id)
        {
            _logger.LogInformation("DELETE order by Guid executing");
            try
            {
                return await _sender.Send(new DeleteOrderCommand(id));
            }
            catch (Exception ex) 
            { 
                _logger.LogError("DELETE by Guid Method Order Error Details: {0}", ex); 
                throw; 
            }
        }
    }
}
