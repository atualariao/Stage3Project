using Moq;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Handlers;
using Test.Moq;
using FluentAssertions;
using S3E1.Entities;
using Shouldly;

namespace UnitTest.Checkout.Commands
{
    public class CheckoutRequestHandlersTest
    {
        private readonly Mock<ICheckoutRepository> _mockRepo;
        private readonly OrderEntity _orderEntity;

        public CheckoutRequestHandlersTest()
        {
            _mockRepo = MockCheckoutRepository.CheckoutRepo();

            _orderEntity = new OrderEntity()
            {
                OrderID = new Guid("28ffe0e3-01a6-4777-8e4c-b0d880ee1d20"),
                UserOrderId = new Guid("dc183fb5-b71b-49a6-b2d5-42ed104ef1c8"),
                OrderCreatedDate = DateTime.Now,
                OrderTotalPrice = 51,
                CartItemEntity = new List<CartItemEntity>()
                   {
                       new CartItemEntity()
                       {
                            ItemID = new Guid("29c99b5b-81ca-49e1-904a-e56a70eb6234"),
                            ItemName = "New Item",
                            ItemPrice = 25.5,
                            ItemStatus = "Processed",
                            OrderEntityOrderID = new Guid("28ffe0e3-01a6-4777-8e4c-b0d880ee1d20")
                       },

                       new CartItemEntity()
                       {
                            ItemID = new Guid("97b8aa2d-fd26-4347-a9db-8d91c26b02df"),
                            ItemName = "New Item 2",
                            ItemPrice = 25.5,
                            ItemStatus = "Processed",
                            OrderEntityOrderID = new Guid("28ffe0e3-01a6-4777-8e4c-b0d880ee1d20")
                       }
                   }
            };
        }

        [Fact]
        public async Task Handle_Should_Checkout_Order()
        {
            var handler = new CheckoutHandler(_mockRepo.Object);

            var result = await handler.Handle(new CheckOutCommand(_orderEntity), CancellationToken.None);

            result.Should().BeOfType<OrderEntity>();
            result.ShouldNotBeNull();
            result.CartItemEntity.Count.Should().Be(2);
            result.CartItemEntity.Should().NotBeNull();

        }
    }
}
