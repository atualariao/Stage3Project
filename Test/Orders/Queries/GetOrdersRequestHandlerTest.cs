//using FluentAssertions;
//using Moq;
//using S3E1.Entities;
//using S3E1.Handlers;
//using S3E1.IRepository;
//using S3E1.Queries;
//using Shouldly;
//using Test.Moq;

//namespace UnitTest.Orders.Queries
//{
//    public class GetOrdersRequestHandlerTest
//    {
//        private readonly Mock<IOrderRepository> _mockRepo;

//        public GetOrdersRequestHandlerTest()
//        {
//            _mockRepo = MockOrderEntityRepository.OrderRepo();
//        }

//        [Fact]
//        public async Task Handle_Should_Get_Orders()
//        {
//            var handler = new GetOrdersHandler(_mockRepo.Object);

//            var result = await handler.Handle(new GetOrdersQuery(), CancellationToken.None);

//            result.Should().BeOfType<List<OrderEntity>>();
//            result.Count.Should().Be(4);
//        }

//        [Fact]
//        public async Task Handle_Should_Get_Order_Id()
//        {
//            var orders = await _mockRepo.Object.GetOrders();

//            var order = orders.FirstOrDefault();

//            var handler = new GetOrdersByIdHandler(_mockRepo.Object);

//            var result = await handler.Handle(new GetOrdersByIdQuery(order.OrderID), CancellationToken.None);

//            result.Should().BeOfType<OrderEntity>();
//            result.OrderID.Should().Be(order.OrderID);
//            result.CartItemEntity.ShouldNotBeNull();
//        }
//    }
//}
