using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.IRepository;

namespace Test.Moq
{
    public static class MockCheckoutRepository
    {
        public static List<CartItemEntity> GenerateItems()
        {
            var items = new Faker<CartItemEntity>()
            .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
            .RuleFor(item => item.ItemName, bogus => bogus.Commerce.ProductName())
            .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double());

            return items.Generate(3);
        }
        public static List<OrderEntity> GenerateOrders()
        {
            var userEntity = new Faker<UserEntity>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            Faker<OrderEntity> orderGenerator = new Faker<OrderEntity>()
                .RuleFor(order => order.OrderID, bogus => bogus.Random.Guid())
                .RuleFor(order => order.UserOrderId, bogus => bogus.Random.Guid())
                .RuleFor(order => order.User, bogus => userEntity)
                .RuleFor(order => order.OrderTotalPrice, bogus => bogus.Random.Double())
                .RuleFor(order => order.OrderCreatedDate, bogus => bogus.Date.Recent())
                .RuleFor(order => order.CartItemEntity, bogus => GenerateItems());

            return orderGenerator.Generate(2);
        }
        public static Mock<ICheckoutRepository> CheckoutRepo()
        {
            var checkoutOrders = GenerateOrders();

            var mockRepo = new Mock<ICheckoutRepository>();

            //Create new user
            mockRepo.Setup(x => x.Checkout(It.IsAny<OrderEntity>())).ReturnsAsync((OrderEntity order) =>
            {
                checkoutOrders.Add(order);
                return order;
            });

            return mockRepo;
        }
    }
}
