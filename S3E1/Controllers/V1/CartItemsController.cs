using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Controllers.V1
{
    [Route("api/cart-items")]
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

        [HttpGet]
        public async Task<List<CartItemEntity>> Get()
        {
            _logger.LogInformation("GET all cart items executing...");
            return await _sender.Send(new GetItemsQuery());
        }

        [HttpGet("{id}")]
        public async Task<CartItemEntity> Get(Guid id)
        {
            _logger.LogInformation("GET cart item by Guid executing...");
            return await _sender.Send(new GetItemByIdQuery(id));
        }

        [HttpPost]
        public async Task<CartItemEntity> Post(CartItemEntity cartItems)
        {
            _logger.LogInformation("POST cart items executing...");
            return await _sender.Send(new AddCartItemCommand(cartItems));
        }

        [HttpPut]
        public async Task<CartItemEntity> Update(CartItemEntity cartItems)
        {
            _logger.LogInformation("PUT/UPDATE cart item executing...");
            return await _sender.Send(new UpdateCartitemCommand(cartItems));
        }

        [HttpDelete("{id}")]
        public async Task<CartItemEntity> Delete(Guid id)
        {
            _logger.LogInformation("DELETE cart item executing...");
            return await _sender.Send(new DeleteCartItemCommand(id));
        }
    }


}
