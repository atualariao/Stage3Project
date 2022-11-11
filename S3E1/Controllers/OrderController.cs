using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3E1.Contracts;
using S3E1.Repository;

namespace S3E1.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository) => _orderRepository = orderRepository;

        [HttpGet]
        public async Task<IActionResult> GerOrders()
        {
            var orderList = await _orderRepository.GerOrders();

            return Ok(orderList);
        }

        [HttpGet("{guid}", Name = "GetOrderById")]
        public async Task<IActionResult> GetUserById(Guid guid)
        {
            var order = await _orderRepository.GetOrderById(guid);
            if (order is null)
                return NotFound();

            return Ok(order);
        }
    }
}
