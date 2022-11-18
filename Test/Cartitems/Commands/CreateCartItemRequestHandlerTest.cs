using AutoMapper;
using Moq;
using S3E1.Commands;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Handlers;
using S3E1.Profiles;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Moq;

namespace Test.Cartitems.Commands
{
    public class CreateCartItemRequestHandlerTest
    {
        private readonly IMapper _mapper;
        private readonly Mock<ICartItemRepository> _mockRepo;
        private readonly CartItems _cartItem;

        public CreateCartItemRequestHandlerTest()
        {
            _mockRepo = MockCartItemRepository.GetCartitems();

            _cartItem = new CartItems()
            {
                ItemName = "Test Item",
                ItemPrice = 45.67,
            };
        }

        [Fact]
        public async Task CreateCartItem()
        {
            var handler = new AddItemsHandler(_mockRepo.Object);

            var result = await handler.Handle(new AddCartItemCommand(_cartItem), CancellationToken.None);

            var cartItems = await _mockRepo.Object.GetCartItems();

            result.ShouldBeOfType<CartItems>();

            cartItems.Count.ShouldBe(3);
        }
    }
}
