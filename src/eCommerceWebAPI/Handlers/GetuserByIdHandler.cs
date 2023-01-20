using MediatR;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Queries;

namespace eCommerceWebAPI.Handlers
{
    public class GetuserByIdHandler : IRequestHandler<GetUserByIdQuery, User>
    {
        private readonly IUserRepository _userRepository;

        public GetuserByIdHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<User> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserById(request.Guid);
        }
    }
}
