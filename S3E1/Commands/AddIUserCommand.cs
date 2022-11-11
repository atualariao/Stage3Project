using MediatR;
using S3E1.Entities;
using S3E1.Data;

namespace S3E1.Commands
{
    //Not applicable to real life scenarios (use DTOs to hide the domain entity from the public API)
    //Not returning a value
    public record AddIUserCommand(UserEntity User) : IRequest<UserEntity>;
}
