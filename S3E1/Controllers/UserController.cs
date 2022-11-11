﻿using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;

namespace S3E1.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ISender _sender;

        public UserController(ISender sender) => _sender = sender;

        [HttpPost]
        public async Task<UserEntity> Post(UserEntity userEntity)
        {
            return await _sender.Send(new AddIUserCommand(userEntity));
        }

        //[HttpGet("{guid}", Name = "GetUserById")]
        //public async Task<IActionResult> GetUserById(Guid guid)
        //{
        //    var cartItem = await _userRepository.GetUserById(guid);
        //    if (cartItem is null)
        //        return NotFound();

        //    return Ok(cartItem);
        //}

        //[HttpPost]
        //public async Task<ActionResult> AddUser([FromBody] UserEntity user)
        //{
        //    var userEntity = await _sender.Send(new AddIUserCommand(user));

        //    return Ok(userEntity);
        //}
    }
}