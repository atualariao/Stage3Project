using FluentAssertions;
using Moq;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using Test.Moq;

namespace UnitTest.Orders.Commands
{
    public class CartItemRequestHandlersTest
    {
        private readonly Mock<IOrderRepository> _mockRepo;

        public CartItemRequestHandlersTest()
        {
            _mockRepo = MockOrderEntityRepository.OrderRepo();
        }

        [Fact]
        public async Task Handle_Should_Update_Order()
        {
            var orders = await _mockRepo.Object.GetOrders();

            foreach (var order in orders)
            {
                var handler = new UpdateOrderHandler(_mockRepo.Object);

                var result = await handler.Handle(new UpdateOrderCommand(order), CancellationToken.None);
            }
            orders.Count.Should().Be(4);
        }

        [Fact]
        public async Task Handle_Should_Delete_Item()
        {
            var orders = await _mockRepo.Object.GetOrders();

            var order = orders.FirstOrDefault();

            var handler = new DeleteOrderHandler(_mockRepo.Object);

            var result = await handler.Handle(new DeleteOrderCommand(order.OrderID), CancellationToken.None);

            result.Should().BeOfType<OrderEntity>();
            orders.Count.Should().Be(3);
        }
    }
}
