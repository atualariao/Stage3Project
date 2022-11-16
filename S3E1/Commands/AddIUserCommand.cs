using MediatR;
using S3E1.Entities;
using S3E1.Data;

namespace S3E1.Commands
{
    public record AddIUserCommand(UserEntity User) : IRequest<UserEntity>;
}
