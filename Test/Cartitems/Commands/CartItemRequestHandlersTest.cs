using Moq;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Handlers;
using Test.Moq;
using FluentAssertions;
using S3E1.Entities;

namespace Test.Cartitems.Commands
{
    public class CartItemRequestHandlersTest
    {
        private readonly Mock<ICartItemRepository> _mockRepo;
        private readonly CartItemEntity _cartItem;

        public CartItemRequestHandlersTest()
        {
            _mockRepo = MockCartItemEntityRepository.CartitemRepo();

            _cartItem = new CartItemEntity()
            {
                ItemID = new Guid("670a0719-64c6-42e7-8c41-cd799dd09d46"),
                ItemName = "Test Item",
                ItemPrice = 45.67,
                ItemStatus = "Pending"
            };
        }

        [Fact]
        public async Task Handle_Should_Add_Item()
        {
            var handler = new AddItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new AddCartItemCommand(_cartItem), CancellationToken.None);

            var cartItems = await _mockRepo.Object.GetCartItems();

            result.Should().BeOfType<CartItemEntity>();

            cartItems.Count.Should().Be(4);
        }

        [Fact]
        public async Task Handle_Should_Update_Item()
        {
            var test = await _mockRepo.Object.GetCartItems();

            var updated = test.Where(x => x.ItemID == new Guid("dd4ebf94-0d42-4a2b-a4d2-d69889f495eb")).First();

            var handler = new UpdateCartItemHandler(_mockRepo.Object);

            var result = await handler.Handle(new UpdateCartitemCommand(updated), CancellationToken.None);

            result.ItemName.Should().Be("Item 3");
        }

        [Fact]
        public async Task Handle_Should_Delete_Item()
        {
            var item = new Guid("dd4ebf94-0d42-4a2b-a4d2-d69889f495eb");

            var handler = new DeleteCartItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new DeleteCartItemCommand(item), CancellationToken.None);

            var itemList = await _mockRepo.Object.GetCartItems();

            result.Should().BeOfType<CartItemEntity>();

            itemList.Count.Should().Be(2);
        }
    }
}
