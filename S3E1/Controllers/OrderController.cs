using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly ISender _sender;

        public OrderController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<List<OrderEntity>> Get()
        {
            return await _sender.Send(new GetOrdersQuery());
        }

        [HttpGet("{id}")]
        public async Task<OrderEntity> GetById(Guid id)
        {
            return await _sender.Send(new GetOrdersByIdQuery(id));
        }

        [HttpPut]
        public async Task<OrderEntity> UpdateOrder(OrderEntity orderEntity)
        {
            return await _sender.Send(new UpdateOrderCommand(orderEntity));
        }

        [HttpDelete("{id}")]
        public async Task<OrderEntity> DeleteOrder(Guid id)
        {
            return await _sender.Send(new DeleteOrderCommand(id));
        }
    }
}
