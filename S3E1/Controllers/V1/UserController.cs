using MediatR;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Controllers.V1
{
    [Route("api/users")]
    [Produces("application/json")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;
        private readonly ILogger<UserController> _logger;

        public UserController(ISender sender, ILogger<UserController> logger)
        {
            _logger = logger;
            _sender = sender;
        }

        [HttpGet("{id}")]
        public async Task<UserEntity> Get(Guid id)
        {
            _logger.LogInformation("GET user by Guid executing...");
            return await _sender.Send(new GetUserByIdQuery(id));
        }

        [HttpPost]
        public async Task<UserEntity> Post(UserEntity users)
        {
            _logger.LogInformation("POST user executing...");
            return await _sender.Send(new AddIUserCommand(users));
        }
    }
}
