using AutoMapper;
using Bogus;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using S3E1.Commands;
using S3E1.Data;
using S3E1.DTOs;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using S3E1.Profiles;
using Shouldly;
using Test.Moq;

namespace UnitTest.Checkout.Commands
{
    public class CheckoutRequestHandlersTest
    {
        public static List<CartItemEntity> GenerateItems()
        {
            var items = new Faker<CartItemEntity>()
            .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
            .RuleFor(item => item.ItemName, bogus => bogus.Commerce.ProductName())
            .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double());

            return items.Generate(3);
        }
        public static OrderEntity GenerateOrder()
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

            return orderGenerator;
        }

        private readonly Mock<ICheckoutRepository> _mockRepo;
        private readonly OrderEntity _orderEntity;
        private readonly IMapper _mapper;
        //private readonly DbContext _dbContext;

        public CheckoutRequestHandlersTest()
        {
            _mockRepo = MockCheckoutRepository.CheckoutRepo();

            _orderEntity = GenerateOrder();

            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<Profiles>();
            });
            _mapper = mapConfig.CreateMapper();

            // Initialize DBContext (inMemory)
            //DbContext dbContextOptionsdbContextOptions = new DbContext();
            //var connection = dbContextOptionsdbContextOptions.UseSqlServer("DefaultConnection");
            //_dbContext = connection.Options;
        }

        [Fact]
        public async Task Handle_Should_Checkout_Order()
        {
            var handler = new CheckoutHandler(_mockRepo.Object, _mapper); // _dbContext

            OrderDTO orderDTO = _mapper.Map<OrderDTO>(_orderEntity);
            var result = await handler.Handle(new CheckOutCommand(orderDTO), CancellationToken.None);

            result.Should().BeOfType<OrderEntity>();
            result.ShouldNotBeNull();
            result.CartItemEntity.Count.Should().Be(3);
            result.CartItemEntity.Should().NotBeNull();

        }
    }
}
