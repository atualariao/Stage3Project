using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.IRepository;

namespace Test.Moq
{
    public static class MockUserRepository
    {
        public static List<UserEntity> UserList = GenerateUsers();
        public static List<UserEntity> GenerateUsers()
        {
            var userEntity = new Faker<UserEntity>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            return userEntity.Generate(4);
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
            mockRepo.Setup(x => x.CreateUser(It.IsAny<UserEntity>())).ReturnsAsync((UserEntity user) =>
            {
                UserList.Add(user);
                return user;
            });

            return mockRepo;
        }
    }
}
