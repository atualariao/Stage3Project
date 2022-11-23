using Moq;
using S3E1.Contracts;
using S3E1.Handlers;
using S3E1.Queries;
using Test.Moq;
using S3E1.DTO;
using FluentAssertions;
using S3E1.Entities;

namespace UnitTest.Users.Queries
{
    public class GetOrdersRequestHandlerTest
    {
        private readonly Mock<IUserRepository> _mockRepo;

        public GetOrdersRequestHandlerTest()
        {
            _mockRepo = MockUserRepository.UserRepo();
        }

        [Fact]
        public async Task Handle_Should_Get_User_Id()
        {
            var user = await _mockRepo.Object.GetUserById(new Guid("f0b4b22b-63f4-4ece-a010-a4ecf8eadcc8"));

            var handler = new GetuserByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetUserByIdQuery(user.UserID), CancellationToken.None);

            result.Should().BeOfType<UserEntity>();
            result.UserID.Should().Be(user.UserID);
        }
    }
}
