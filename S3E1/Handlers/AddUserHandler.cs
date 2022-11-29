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
        private readonly ILogger<AddUserHandler> _logger;
        public AddUserHandler(IUserRepository userRepository, ILogger<AddUserHandler> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Handle(AddIUserCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("New User Created in the Database, Object: {0}", JsonConvert.SerializeObject(request.Users).ToUpper());
            return await _userRepository.CreateUser(request.Users);
        }
    }
}
