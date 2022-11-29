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

        public CartItemsController(ISender sender) => _sender = sender;

        [HttpGet]
        public async Task<List<CartItemEntity>> Get()
        {
            return await _sender.Send(new GetItemsQuery());
        }

        [HttpGet("{id}")]
        public async Task<CartItemEntity> Get(Guid id)
        {
            return await _sender.Send(new GetItemByIdQuery(id));
        }

        [HttpPost]
        public async Task<CartItemEntity> Post(CartItemEntity cartItems)
        {
            return await _sender.Send(new AddCartItemCommand(cartItems));
        }

        [HttpPut]
        public async Task<CartItemEntity> Update(CartItemEntity cartItems)
        {
            return await _sender.Send(new UpdateCartitemCommand(cartItems));
        }

        [HttpDelete("{id}")]
        public async Task<CartItemEntity> Delete(Guid id)
        {
            return await _sender.Send(new DeleteCartItemCommand(id));
        }
    }


}
