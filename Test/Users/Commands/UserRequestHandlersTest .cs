using AutoMapper;
using Bogus;
using FluentAssertions;
using Moq;
using S3E1.Commands;
using S3E1.Configurations;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using Shouldly;
using Test.Moq;

namespace UnitTest.Users.Commands
{
    public class CartItemRequestHandlersTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly User _User;

        public static User CreateNewUser()
        {
            var newUser = new Faker<User>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            return newUser;
        }
        public CartItemRequestHandlersTest()
        {
            _mockRepo = MockUserRepository.UserRepo();
            _User = CreateNewUser();

            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<AutoMapperInitializer>();
            });
            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Create_User()
        {
            var handler = new AddUserHandler(_mockRepo.Object, _mapper);

            UserDTO userDTO = _mapper.Map<UserDTO>(_User);

            var result = await handler.Handle(new AddIUserCommand(userDTO), CancellationToken.None);

            result.ShouldNotBeNull();
            result.Should().BeOfType<User>();
        }
    }
}
