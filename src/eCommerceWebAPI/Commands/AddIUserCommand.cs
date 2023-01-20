using MediatR;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Commands
{
    public record AddIUserCommand(CreateUserDTO newUser) : IRequest<User>;
}
