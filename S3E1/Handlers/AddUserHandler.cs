using MediatR;
using Newtonsoft.Json;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class AddUserHandler : IRequestHandler<AddIUserCommand, UserEntity>
    {
        private readonly IUserRepository _userRepository;
        public AddUserHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserEntity> Handle(AddIUserCommand request, CancellationToken cancellationToken)
        {
            return await _userRepository.CreateUser(request.Users);
        }
    }
}
