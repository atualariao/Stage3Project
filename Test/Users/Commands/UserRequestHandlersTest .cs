using Bogus;
using FluentAssertions;
using Moq;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using Shouldly;
using Test.Moq;

namespace UnitTest.Users.Commands
{
    public class CartItemRequestHandlersTest
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserEntity _userEntity;

        public static UserEntity CreateNewUser()
        {
            var newUserEntity = new Faker<UserEntity>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            return newUserEntity;
        }
        public CartItemRequestHandlersTest()
        {
            _mockRepo = MockUserRepository.UserRepo();
            _userEntity = CreateNewUser();
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
