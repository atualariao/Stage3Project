using Moq;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test.Moq
{
    public static class MockCartItemRepository
    {
        public static Mock<ICartItemRepository> GetCartitems()
        {
            var items = new List<CartItems>
            {
                new CartItems
                {
                    ItemName = "item 1",
                    ItemPrice = 34.56,
                },

                new CartItems
                {
                    ItemName = "Item 2",
                    ItemPrice = 69.69,
                }
            };

            var mockRepo = new Mock<ICartItemRepository>();

            mockRepo.Setup(x => x.GetCartItems()).ReturnsAsync(items);

            mockRepo.Setup(x => x.Createitem(It.IsAny<CartItems>())).ReturnsAsync((CartItems cart) =>
            {
                items.Add(cart);
                return cart;
            });

            return mockRepo;
        }
    }
}
