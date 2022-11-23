using MediatR;
using S3E1.Entities;
using S3E1.DTO;

namespace S3E1.Commands
{
    public record AddIUserCommand(UserEntity Users) : IRequest<UserEntity>;
}
