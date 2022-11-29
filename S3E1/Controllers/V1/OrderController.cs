using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Controllers.V1
{
    [Route("api/orders")]
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
            return await _sender.Send(new GetOrdersQuery());
        }

        [HttpGet("{id}")]
        public async Task<OrderEntity> GetById(Guid id)
        {
            _logger.LogInformation("GET order by Guid executing");
            return await _sender.Send(new GetOrdersByIdQuery(id));
        }

        [HttpPut]
        public async Task<OrderEntity> UpdateOrder(OrderEntity orders)
        {
            _logger.LogInformation("PUT/UPDATE order executing");
            return await _sender.Send(new UpdateOrderCommand(orders));
        }

        [HttpDelete("{id}")]
        public async Task<OrderEntity> DeleteOrder(Guid id)
        {
            _logger.LogInformation("DELETE order by Guid executing");
            return await _sender.Send(new DeleteOrderCommand(id));
        }
    }
}
