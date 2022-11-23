using MediatR;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using S3E1.Queries;

namespace S3E1.Handlers
{
    public class GetuserByIdHandler : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        private readonly IUserRepository _userRepository;

        public GetuserByIdHandler(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            return await _userRepository.GetUserById(request.Guid);
        }
    }
}
