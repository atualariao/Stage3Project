using Moq;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Handlers;
using Test.Moq;
using FluentAssertions;
using S3E1.Entities;

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

            var updated = orders.Where(x => x.OrderID == new Guid("2daa4319-933e-4eea-9123-1e1877d8aa01")).First();

            var handler = new UpdateOrderHandler(_mockRepo.Object);

            var result = await handler.Handle(new UpdateOrderCommand(updated), CancellationToken.None);

            result.CartItemEntity.Should().BeOfType<List<CartItemEntity>>();
            result.CartItemEntity.Count.Should().Be(2);
            orders.Count.Should().Be(2);
        }

        [Fact]
        public async Task Handle_Should_Delete_Item()
        {
            var orders = await _mockRepo.Object.GetOrders();

            var item = orders.Where(x => x.OrderID == new Guid("2daa4319-933e-4eea-9123-1e1877d8aa01")).First();

            var handler = new DeleteOrderHandler(_mockRepo.Object);

            var result = await handler.Handle(new DeleteOrderCommand(item.OrderID), CancellationToken.None);

            result.Should().BeOfType<OrderEntity>();
            orders.Count.Should().Be(1);
        }
    }
}
