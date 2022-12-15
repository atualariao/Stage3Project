using AutoMapper;
using FluentAssertions;
using Moq;
using S3E1.Configurations;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using S3E1.Queries;
using Shouldly;
using Test.Moq;

namespace UnitTest.Orders.Queries
{
    public class GetOrdersRequestHandlerTest
    {
        private readonly Mock<IOrderRepository> _mockRepo;
        private readonly IMapper _mapper;

        public GetOrdersRequestHandlerTest()
        {
            _mockRepo = MockOrderRepository.OrderRepo();

            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<AutoMapperInitializer>();
            });
            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Get_Orders()
        {
            var handler = new GetOrdersHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetOrdersQuery(), CancellationToken.None);

            result.Should().BeOfType<List<OrderDTO>>();
            result.Count.Should().Be(4);
        }

        [Fact]
        public async Task Handle_Should_Get_Order_Id()
        {
            var orders = await _mockRepo.Object.GetOrders();

            var order = orders.FirstOrDefault();

            var handler = new GetOrdersByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetOrdersByIdQuery(order.PrimaryID), CancellationToken.None);

            result.Should().BeOfType<Order>();
            result.PrimaryID.Should().Be(order.PrimaryID);
            result.CartItemEntity.ShouldNotBeNull();
        }
    }
}
