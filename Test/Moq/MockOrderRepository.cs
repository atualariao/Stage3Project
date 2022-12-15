using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.IRepository;

namespace Test.Moq
{
    public static class MockOrderRepository
    {
        public static List<CartItem> GenerateItems()
        {
            var items = new Faker<CartItem>()
            .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
            .RuleFor(item => item.ItemName, bogus => bogus.Commerce.ProductName())
            .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double());

            return items.Generate(2);
        }
        public static List<Order> GenerateOrders()
        {
            var User = new Faker<User>()
                .RuleFor(user => user.UserID, bogus => bogus.Random.Guid())
                .RuleFor(user => user.Username, bogus => bogus.Name.FullName());

            Faker<Order> orderGenerator = new Faker<Order>()
                .RuleFor(order => order.PrimaryID, bogus => bogus.Random.Guid())
                .RuleFor(order => order.UserPrimaryID, bogus => bogus.Random.Guid())
                .RuleFor(order => order.User, bogus => User)
                .RuleFor(order => order.OrderTotalPrice, bogus => bogus.Random.Double())
                .RuleFor(order => order.OrderCreatedDate, bogus => bogus.Date.Recent())
                .RuleFor(order => order.CartItemEntity, bogus => GenerateItems());

            return orderGenerator.Generate(4);
        }

        public static Mock<IOrderRepository> OrderRepo()
        {
            var orders = GenerateOrders();

            var mockRepo = new Mock<IOrderRepository>();

            //Get all orders
            mockRepo.Setup(x => x.GetOrders()).ReturnsAsync(orders);

            //Get specific order (by Id)
            mockRepo.Setup(x => x.GetOrderById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                return orders.First(id => id.PrimaryID == guid);
            });

            //Update Existing order (object)
            mockRepo.Setup(x => x.UpdateOrder(It.IsAny<Order>())).ReturnsAsync((Order order) =>
            {
                return order;
            });

            //Delete Order
            mockRepo.Setup(x => x.DeleteOrderById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                var item = orders.First(id => id.PrimaryID == guid);
                orders.Remove(item);
                return item;
            });

            return mockRepo;
        }
    }
}
