using Moq;
using S3E1.Contracts;
using S3E1.Handlers;
using S3E1.Queries;
using Test.Moq;
using S3E1.DTO;
using FluentAssertions;
using S3E1.Entities;

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
            result.Count.Should().Be(3);
        }

        [Fact]
        public async Task Handle_Should_Get_Item_Id()
        {
            var items = await _mockRepo.Object.GetCartItems();

            var item = items.Where(x => x.ItemID == new Guid("dd4ebf94-0d42-4a2b-a4d2-d69889f495eb")).First();

            var handler = new GetItemByIdHandler(_mockRepo.Object);

            var result = await handler.Handle(new GetItemByIdQuery(item.ItemID), CancellationToken.None);

            result.Should().BeOfType<CartItemEntity>();
            result.ItemID.Should().Be(item.ItemID);
        }
    }
}
