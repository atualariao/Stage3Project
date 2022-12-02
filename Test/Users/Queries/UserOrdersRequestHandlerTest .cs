using FluentAssertions;
using Moq;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using S3E1.Queries;
using Test.Moq;

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
            var userList = MockUserRepository.UserList;

            var user = userList.FirstOrDefault();

            var handler = new GetuserByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetUserByIdQuery(user.UserID), CancellationToken.None);

            result.Should().BeOfType<UserEntity>();
            result.UserID.Should().Be(user.UserID);
            userList.Count.Should().Be(4);
        }
    }
}
