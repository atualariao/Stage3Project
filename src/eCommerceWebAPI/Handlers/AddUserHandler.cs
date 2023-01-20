using AutoMapper;
using MediatR;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;

namespace eCommerceWebAPI.Handlers
{
    public class AddUserHandler : IRequestHandler<AddIUserCommand, User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public AddUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public async Task<User> Handle(AddIUserCommand request, CancellationToken cancellationToken)
        {
            var user = new User()
            {
                Username = request.newUser.Username
            };

            return await _userRepository.CreateUser(user);
        }
    }
}
