using MediatR;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;

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
