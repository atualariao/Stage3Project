using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Queries;
using Swashbuckle.AspNetCore.Annotations;

namespace S3E1.Controllers.V1
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
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

        [SwaggerOperation(
            Summary = "Returns a specific user",
            Description = "Returns a specific user")]
        [HttpGet("{UserID}")]
        public async Task<User> Get(Guid UserID)
        {
            _logger.LogInformation("GET user by Guid executing...");
            try
            {
                return await _sender.Send(new GetUserByIdQuery(UserID));
            }
            catch (Exception ex)
            {
                _logger.LogError("GET by Guid Method User Error Details: {0}", ex);
                throw;
            }
        }

        [SwaggerOperation(
            Summary = "Creates a new user",
            Description = "Creates a new user")]
        [HttpPost]
        public async Task<User> Post([FromBody]CreateUserDTO users)
        {
            _logger.LogInformation("POST user executing...");
            try
            {
                return await _sender.Send(new AddIUserCommand(users));
            }
            catch (Exception ex)
            {
                _logger.LogError("POST Method User Error Details: {0}", ex);
                throw;
            }
        }
    }
}
