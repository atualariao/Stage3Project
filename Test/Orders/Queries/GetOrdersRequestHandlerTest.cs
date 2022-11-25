using Moq;
using S3E1.Contracts;
using S3E1.Handlers;
using S3E1.Queries;
using Test.Moq;
using S3E1.DTO;
using FluentAssertions;
using S3E1.Entities;
using Shouldly;

namespace UnitTest.Orders.Queries
{
    public class GetOrdersRequestHandlerTest
    {
        private readonly Mock<IOrderRepository> _mockRepo;

        public GetOrdersRequestHandlerTest()
        {
            _mockRepo = MockOrderEntityRepository.OrderRepo();
        }

        [Fact]
        public async Task Handle_Should_Get_Orders()
        {
            var handler = new GetOrdersHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetOrdersQuery(), CancellationToken.None);

            result.Should().BeOfType<List<OrderEntity>>();
            result.Count.Should().Be(2);
        }

        [Fact]
        public async Task Handle_Should_Get_Order_Id()
        {
            var orders = await _mockRepo.Object.GetOrders();

            var order = orders.Where(id => id.OrderID == new Guid("d43866ae-89c1-445a-bcac-1ac1eebd0cae")).First();

            var handler = new GetOrdersByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetOrdersByIdQuery(order.OrderID), CancellationToken.None);

            result.Should().BeOfType<OrderEntity>();
            result.OrderID.Should().Be(order.OrderID);
            result.CartItemEntity.ShouldNotBeNull();
        }
    }
}
