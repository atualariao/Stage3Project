using AutoMapper;
using FluentAssertions;
using Moq;
using eCommerceWebAPI.Configurations;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Handlers;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Queries;
using Test.Moq;

namespace Test.Cartitems.Queries
{
    public class GetCartItemsRequestHandlerTest
    {
        private readonly Mock<ICartItemRepository> _mockRepo;
        private readonly IMapper _mapper;

        public GetCartItemsRequestHandlerTest()
        {
            _mockRepo = MockCartItemRepository.CartitemRepo();
            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<AutoMapperInitializer>();
            });
            _mapper = mapConfig.CreateMapper();
        }

        [Fact]
        public async Task Handle_Should_Get_Items()
        {
            var handler = new GetItemsHandler(_mockRepo.Object, _mapper);

            var result = await handler.Handle(new GetItemsQuery(), CancellationToken.None);

            result.Should().BeOfType<List<CartItemDTO>>();
            result.Count.Should().Be(4);
        }

        [Fact]
        public async Task Handle_Should_Get_Item_Id()
        {
            var items = await _mockRepo.Object.GetCartItems();

            var item = items.FirstOrDefault();

            var handler = new GetItemByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetItemByIdQuery(item.ItemID), CancellationToken.None);

            result.Should().BeOfType<CartItem>();
            result.ItemID.Should().Be(item.ItemID);
        }
    }
}
