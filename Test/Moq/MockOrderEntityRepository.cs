using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.IRepository;

namespace Test.Moq
{
    public static class MockOrderEntityRepository
    {
        public static List<CartItemEntity> GenerateItems()
        {
            var items = new Faker<CartItemEntity>()
            .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
            .RuleFor(item => item.ItemName, bogus => bogus.Commerce.ProductName())
            .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double());

            return items.Generate(2);
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
                return orders.First(id => id.OrderID == guid);
            });

            //Update Existing order (object)
            mockRepo.Setup(x => x.UpdateOrder(It.IsAny<OrderEntity>())).ReturnsAsync((OrderEntity order) =>
            {
                return order;
            });

            //Delete Order
            mockRepo.Setup(x => x.DeleteOrderById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                var item = orders.First(id => id.OrderID == guid);
                orders.Remove(item);
                return item;
            });

            return mockRepo;
        }
    }
}
