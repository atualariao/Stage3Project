using AutoMapper;
using FluentAssertions;
using Moq;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Handlers;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Entities;
using Shouldly;
using Test.Moq;
using eCommerceWebAPI.Configurations;
using eCommerceWebAPI.Enumerations;

namespace UnitTest.Checkout.Commands
{
    public class CheckoutRequestHandlersTest
    {

        private readonly Mock<ICheckoutRepository> _mockRepo;
        private readonly IMapper _mapper;

        public CheckoutRequestHandlersTest()
        {
            _mockRepo = MockCheckoutRepository.CheckoutRepo();

            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<AutoMapperInitializer>();
            });
            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Checkout_Order()
        {
            var handler = new CheckoutHandler(_mockRepo.Object, _mapper);

            var order = MockCheckoutRepository.GenerateOrder();

            OrderDTO orderDTO = _mapper.Map<OrderDTO>(order);

            orderDTO.OrderStatus = OrderStatus.Processed;

            var result = await handler.Handle(new CheckOutCommand(orderDTO), CancellationToken.None);

            OrderDTO resultDTO = _mapper.Map<OrderDTO>(result);

            resultDTO.PrimaryID = orderDTO.PrimaryID;
            resultDTO.OrderCreatedDate = orderDTO.OrderCreatedDate;
            resultDTO.OrderCreatedDate = orderDTO.OrderCreatedDate;
            resultDTO.OrderStatus = orderDTO.OrderStatus;
            resultDTO.CartItemEntity = orderDTO.CartItemEntity;

            resultDTO.Should().BeOfType<OrderDTO>();
            resultDTO.ShouldNotBeNull();
            resultDTO.OrderStatus.Should().Be(orderDTO.OrderStatus);
            resultDTO.UserPrimaryID.Should().Be(orderDTO.UserPrimaryID);
            resultDTO.CartItemEntity.Should().NotBeNull();
            resultDTO.CartItemEntity.Count.Should().Be(3);
        }
    }
}
