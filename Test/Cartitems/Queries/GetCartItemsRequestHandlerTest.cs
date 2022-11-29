using FluentAssertions;
using Moq;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using S3E1.Queries;
using Test.Moq;

namespace Test.Cartitems.Queries
{
    public class GetCartItemsRequestHandlerTest
    {
        private readonly Mock<ICartItemRepository> _mockRepo;

        public GetCartItemsRequestHandlerTest()
        {
            _mockRepo = MockCartItemEntityRepository.CartitemRepo();
        }

        [Fact]
        public async Task Handle_Should_Get_Items()
        {
            var handler = new GetItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetItemsQuery(), CancellationToken.None);

            result.Should().BeOfType<List<CartItemEntity>>();
            result.Count.Should().Be(4);
        }

        [Fact]
        public async Task Handle_Should_Get_Item_Id()
        {
            var items = await _mockRepo.Object.GetCartItems();

            var item = items.FirstOrDefault();

            var handler = new GetItemByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetItemByIdQuery(item.ItemID), CancellationToken.None);

            result.Should().BeOfType<CartItemEntity>();
            result.ItemID.Should().Be(item.ItemID);
        }
    }
}
