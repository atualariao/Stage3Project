using Azure;
using Azure.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender) => _sender = sender;

        [HttpGet("{id}")]
        public async Task<UserEntity> Get(Guid id)
        {
            return await _sender.Send(new GetUserByIdQuery(id));
        }

        [HttpPost]
        public async Task<UserEntity> Post(UserEntity users)
        {
            return await _sender.Send(new AddIUserCommand(users));
        }
    }
}
