using MediatR;
using S3E1.Contracts;
using S3E1.Entities;
using S3E1.Queries;
using S3E1.Repository;

namespace S3E1.Handlers
{
    public class GetuserByIdHandler : IRequestHandler<GetUserByIdQuery, UserEntity>
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<GetuserByIdHandler> _logger;

        public GetuserByIdHandler(IUserRepository userRepository, ILogger<GetuserByIdHandler> logger)
        {
            _logger = logger;
            _userRepository = userRepository;
        }

        public async Task<UserEntity> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("User retrieved from database, Guid: {0}", request.Guid.ToString().ToUpper());
            return await _userRepository.GetUserById(request.Guid);
        }
    }
}
