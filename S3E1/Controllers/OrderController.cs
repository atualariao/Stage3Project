using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
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
        public async Task<OrderEntity> UpdateOrder(OrderEntity orders)
        {
            return await _sender.Send(new UpdateOrderCommand(orders));
        }

        [HttpDelete("{id}")]
        public async Task<OrderEntity> DeleteOrder(Guid id)
        {
            return await _sender.Send(new DeleteOrderCommand(id));
        }
    }
}
