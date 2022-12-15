using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.IRepository;
using S3E1.Enumerations;

namespace Test.Moq
{
    public static class MockCheckoutRepository
    {
        public static List<CartItem> GenerateItems()
        {
            var items = new Faker<CartItem>()
            .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
            .RuleFor(item => item.ItemName, bogus => bogus.Commerce.ProductName())
            .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double());

            return items.Generate(3);
        }
        public static Order GenerateOrder()
        {
            var User = new Faker<User>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            Faker<Order> orderGenerator = new Faker<Order>()
                .RuleFor(order => order.PrimaryID, bogus => bogus.Random.Guid())
                .RuleFor(order => order.UserPrimaryID, bogus => bogus.Random.Guid())
                .RuleFor(order => order.User, bogus => User)
                .RuleFor(order => order.OrderStatus, OrderStatus.Pending)
                .RuleFor(order => order.OrderTotalPrice, bogus => bogus.Random.Double())
                .RuleFor(order => order.OrderCreatedDate, bogus => bogus.Date.Recent())
                .RuleFor(order => order.CartItemEntity, bogus => GenerateItems());

            return orderGenerator.Generate();
        }
        public static Mock<ICheckoutRepository> CheckoutRepo()
        {
            var checkoutOrders = GenerateOrder();

            var mockRepo = new Mock<ICheckoutRepository>();

            //Create new user
            mockRepo.Setup(x => x.Checkout(It.IsAny<Order>())).ReturnsAsync((Order order) =>
            {
                return order;
            });

            return mockRepo;
        }
    }
}
