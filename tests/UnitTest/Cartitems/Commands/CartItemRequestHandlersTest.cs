using AutoMapper;
using FluentAssertions;
using Moq;
using eCommerceWebAPI.Commands;
using eCommerceWebAPI.Configurations;
using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;
using eCommerceWebAPI.Handlers;
using eCommerceWebAPI.Interface;
using Test.Moq;

namespace Test.Cartitems.Commands
{
    public class CartItemRequestHandlersTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICartItemRepository> _mockRepo;
        private readonly CartItem _cartItem;

        public CartItemRequestHandlersTest()
        {
            _mockRepo = MockCartItemRepository.CartitemRepo();

            _cartItem = new CartItem()
            {
                ItemID = Guid.NewGuid(),
                ItemName = "Test Item",
                ItemPrice = 45.67,
                CustomerID = Guid.NewGuid(),
            };

            MapperConfiguration mapConfig = new(c =>
            {
                c.AddProfile<AutoMapperInitializer>();
            });
            _mapper = mapConfig.CreateMapper();

        }

        [Fact]
        public async Task Handle_Should_Add_Item()
        {
            var handler = new AddItemsHandler(_mockRepo.Object, _mapper);
            CartItemDTO cartItemDTO = _mapper.Map<CartItemDTO>(_cartItem);

            var userGuid = Guid.NewGuid();

            var result = await handler.Handle(new AddCartItemCommand(cartItemDTO, userGuid), CancellationToken.None);

            var cartItems = await _mockRepo.Object.GetCartItems();

            result.Should().BeOfType<CartItem>();
            cartItems.Count.Should().Be(5);
        }

        [Fact]
        public async Task Handle_Should_Update_Item()
        {
            var itemlist = await _mockRepo.Object.GetCartItems();

            foreach (var item in itemlist)
            {
                var handler = new UpdateCartItemHandler(_mockRepo.Object, _mapper);

                CartItemDTO cartItem = _mapper.Map<CartItemDTO>(item);

                cartItem.ItemName = "Updated Item Name";
                cartItem.ItemPrice = 420.69;

                var result = await handler.Handle(new UpdateCartitemCommand(cartItem), CancellationToken.None);

                result.ItemID.Should().Be(item.ItemID);
                result.ItemName.Should().Be(cartItem.ItemName);
                result.ItemPrice.Should().Be(cartItem.ItemPrice);
            }
        }

        [Fact]
        public async Task Handle_Should_Delete_Item()
        {
            var itemList = await _mockRepo.Object.GetCartItems();

            var itemToDelete = itemList.FirstOrDefault();

            var handler = new DeleteCartItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new DeleteCartItemCommand(itemToDelete.ItemID), CancellationToken.None);

            result.Should().BeOfType<CartItem>();
            itemList.Count.Should().Be(3);

        }
    }
}
