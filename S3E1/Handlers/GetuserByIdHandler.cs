using MediatR;
using S3E1.Entities;
using S3E1.Interface;
using S3E1.Queries;

namespace S3E1.Handlers
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
