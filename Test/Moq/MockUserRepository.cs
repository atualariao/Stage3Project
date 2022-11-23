using Moq;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;

namespace Test.Moq
{
    public static class MockUserRepository
    {
        public static Mock<IUserRepository> UserRepo()
        {
            var users = new List<UserEntity>
            {
                new UserEntity
                {
                     UserID = new Guid("0c82cd53-b718-4416-ae61-54a419eac438"),
                     Username = "User 1",
                },
                
                new UserEntity
                {
                     UserID = new Guid("f0b4b22b-63f4-4ece-a010-a4ecf8eadcc8"),
                     Username = "User 2",
                },

                new UserEntity
                {
                     UserID = new Guid("1739d5be-0bdc-490b-a524-d30def23b671"),
                     Username = "User 3",
                },
            };

            var mockRepo = new Mock<IUserRepository>();

            //Get specific order (by Id)
            mockRepo.Setup(x => x.GetUserById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                return users.First(id => id.UserID == guid);
            });

            //Create new user
            mockRepo.Setup(x => x.CreateUser(It.IsAny<UserEntity>())).ReturnsAsync((UserEntity user) =>
            {
                users.Add(user);
                return user;
            });

            return mockRepo;
        }
    }
}
