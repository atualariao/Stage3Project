using Moq;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;

namespace Test.Moq
{
    public static class MockCheckoutRepository
    {
        public static Mock<ICheckoutRepository> CheckoutRepo()
        {
            var checkoutOrders = new List<OrderEntity>
            {
                new OrderEntity
                {
                    OrderID = new Guid("4335a879-4ae8-4972-bf74-5f922e7b9f6a"),
                    UserOrderId = new Guid("a5df0927-baf8-4af4-830c-65402d80ba0d"),
                    OrderTotalPrice = 1000.10,
                    OrderCreatedDate = DateTime.Now,
                    CartItemEntity = new List<CartItemEntity>()
                    {
                        new CartItemEntity
                        {
                            ItemID = new Guid("4ae304be-cba7-483c-89d1-7695f18b2f9e"),
                            ItemName = "Item 1",
                            ItemPrice = 500.25,
                            ItemStatus = "Processed",
                            OrderEntityOrderID = new Guid("4335a879-4ae8-4972-bf74-5f922e7b9f6a")
                        },

                        new CartItemEntity
                        {
                            ItemID = new Guid("f1dc6760-267e-4f22-ab57-7aa5d22c4fe1"), 
                            ItemName = "Item 2",
                            ItemPrice = 500.25,
                            ItemStatus = "Processed",
                            OrderEntityOrderID = new Guid("4335a879-4ae8-4972-bf74-5f922e7b9f6a")
                        }
                    }
                }
            };

            var mockRepo = new Mock<ICheckoutRepository>();

            //Create new user
            mockRepo.Setup(x => x.Checkout(It.IsAny<OrderEntity>())).ReturnsAsync((OrderEntity order) =>
            {
                checkoutOrders.Add(order);
                return order;
            });

            return mockRepo;
        }
    }
}
