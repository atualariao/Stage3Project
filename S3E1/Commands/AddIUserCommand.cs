﻿using MediatR;
using S3E1.DTOs;
using S3E1.Entities;

namespace S3E1.Commands
{
    public record AddIUserCommand(UserDTO newUser) : IRequest<UserEntity>;
}
