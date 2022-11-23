using Moq;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Handlers;
using Test.Moq;
using FluentAssertions;
using S3E1.Entities;
using Shouldly;

namespace UnitTest.Users.Commands
{
    public class CartItemRequestHandlersTest
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserEntity _userEntity;

        public CartItemRequestHandlersTest()
        {
            _mockRepo = MockUserRepository.UserRepo();
            _userEntity = new UserEntity()
            {
                UserID = new Guid("483cbf3c-17b4-4668-af61-6c3ecaa21416"),
                Username = "New Test User"
            };
        }

        [Fact]
        public async Task Handle_Should_Create_User()
        {
            var handler = new AddUserHandler(_mockRepo.Object);

            var result = await handler.Handle(new AddIUserCommand(_userEntity), CancellationToken.None);

            result.ShouldNotBeNull();
            result.Should().BeOfType<UserEntity>();
        }
    }
}
