using AutoMapper;
using MediatR;
using S3E1.Commands;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.IRepository;

namespace S3E1.Handlers
{
    public class AddUserHandler : IRequestHandler<AddIUserCommand, UserEntity>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Handle(AddIUserCommand request, CancellationToken cancellationToken)
        {
            var user = new UserDTO()
            {
                UserID = Guid.NewGuid(),
                Username = request.newUser.Username
            };

            UserEntity userEntity = _mapper.Map<UserEntity>(user);
            return await _userRepository.CreateUser(userEntity);
        }
    }
}
