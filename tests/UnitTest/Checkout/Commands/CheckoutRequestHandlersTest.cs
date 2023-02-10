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
using Azure;

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

            var userGuid = Guid.NewGuid();

            var result = await handler.Handle(new CheckOutCommand(userGuid), CancellationToken.None);

            result.ShouldBeOfType<Guid>();
        }
    }
}
