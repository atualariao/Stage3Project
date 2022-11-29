using FluentAssertions;
using Moq;
using S3E1.Commands;
using S3E1.Entities;
using S3E1.Handlers;
using S3E1.IRepository;
using Test.Moq;

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
                ItemID = Guid.NewGuid(),
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

            cartItems.Count.Should().Be(5);
        }

        [Fact]
        public async Task Handle_Should_Update_Item()
        {
            var itemlist = await _mockRepo.Object.GetCartItems();

            foreach (var item in itemlist)
            {
                var handler = new UpdateCartItemHandler(_mockRepo.Object);

                var result = await handler.Handle(new UpdateCartitemCommand(item), CancellationToken.None);

                result.ItemID.Should().Be(item.ItemID);
                result.ItemName.Should().Be(item.ItemName);
                result.ItemPrice.Should().Be(item.ItemPrice);
            }
        }

        [Fact]
        public async Task Handle_Should_Delete_Item()
        {
            var itemList = await _mockRepo.Object.GetCartItems();

            var itemToDelete = itemList.FirstOrDefault();

            var handler = new DeleteCartItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new DeleteCartItemCommand(itemToDelete.ItemID), CancellationToken.None);

            result.Should().BeOfType<CartItemEntity>();
            itemList.Count.Should().Be(3);

        }
    }
}
