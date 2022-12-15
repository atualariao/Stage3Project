using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.IRepository;

namespace Test.Moq
{
    public static class MockUserRepository
    {
        public static List<User> UserList = GenerateUsers();
        public static List<User> GenerateUsers()
        {
            var User = new Faker<User>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            return User.Generate(4);
        }
        public static Mock<IUserRepository> UserRepo()
        {


            var mockRepo = new Mock<IUserRepository>();

            //Get specific order (by Id)
            mockRepo.Setup(x => x.GetUserById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                return UserList.First(id => id.UserID == guid);
            });

            //Create new user
            mockRepo.Setup(x => x.CreateUser(It.IsAny<User>())).ReturnsAsync((User user) =>
            {
                UserList.Add(user);
                return user;
            });

            return mockRepo;
        }
    }
}
