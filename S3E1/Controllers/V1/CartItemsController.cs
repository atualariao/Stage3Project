using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.DTOs;
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
            try
            {
                return await _sender.Send(new GetItemsQuery());
            }
            catch (Exception ex)
            {
                _logger.LogError("GET Method Error Details: {0}", ex);
                throw;
            }
        }

        [HttpGet("{id}")]
        public async Task<CartItemEntity> Get(Guid id)
        {
            _logger.LogInformation("GET cart item by Guid executing...");
            try
            {
                return await _sender.Send(new GetItemByIdQuery(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("GET Cart By Item Guid Error Details: {0}", ex);
                throw;
            }
        }

        [HttpPost]
        public async Task<CartItemEntity> Post(CartItemDTO cartItems)
        {
            _logger.LogInformation("POST cart items executing...");
            try
            {
                return await _sender.Send(new AddCartItemCommand(cartItems));
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Cart Item Error Details: {0}", ex);
                throw;
            }
        }

        [HttpPut]
        public async Task<CartItemEntity> Update(CartItemDTO cartItems)
        {
            _logger.LogInformation("PUT/UPDATE cart item executing...");
            try
            {
                return await _sender.Send(new UpdateCartitemCommand(cartItems));
            }
            catch (Exception ex)
            {
                _logger.LogError("PUT/UPDATE Cart Item Error Details: {0}", ex);
                throw;
            }
        }

        [HttpDelete("{id}")]
        public async Task<CartItemEntity> Delete(Guid id)
        {
            _logger.LogInformation("DELETE cart item executing...");
            try
            {
                return await _sender.Send(new DeleteCartItemCommand(id));
            }
            catch (Exception ex)
            {
                _logger.LogError("DELETE Cart Item Error Details: {0}", ex);
                throw;
            }
        }
    }


}
