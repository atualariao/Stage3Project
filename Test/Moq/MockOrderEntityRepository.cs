using Moq;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;

namespace Test.Moq
{
    public static class MockOrderEntityRepository
    {
        public static Mock<IOrderRepository> OrderRepo()
        {
            var orders = new List<OrderEntity>
            {
                new OrderEntity
                {
                     OrderID = new Guid("2daa4319-933e-4eea-9123-1e1877d8aa01"),
                     UserOrderId = new Guid("1b05a316-a044-444d-a81b-02f92cb4ab9d"),
                     OrderTotalPrice = 23.54,
                     OrderCreatedDate = DateTime.Now,
                     CartItemEntity = new()
                      {
                         new CartItemEntity()
                         {
                             ItemID = new Guid("4f7cd377-4eda-4b27-bc9b-f28cdc401b26"),
                             ItemName = "Item 1",
                             ItemPrice = 45.65,
                             ItemStatus = "Pending",
                             OrderEntityOrderID = new Guid("2daa4319-933e-4eea-9123-1e1877d8aa01")
                         },

                         new CartItemEntity()
                         {
                             ItemID = new Guid("96021179-8911-452f-8816-948ebb244732"),
                             ItemName = "Item 2",
                             ItemPrice = 65.87,
                             ItemStatus = "Pending",
                             OrderEntityOrderID = new Guid("2daa4319-933e-4eea-9123-1e1877d8aa01")
                         }
                      }
                },

                new OrderEntity
                {
                    OrderID = new Guid("d43866ae-89c1-445a-bcac-1ac1eebd0cae"),
                    UserOrderId = new Guid("1b05a316-a044-444d-a81b-02f92cb4ab9d"),
                    OrderTotalPrice = 54.87,
                    OrderCreatedDate = DateTime.Now,
                    CartItemEntity = new()
                     {
                        new CartItemEntity()
                        {
                            ItemID = new Guid("2ddc05a2-e9cd-41fc-8076-6ca747231ff1"),
                            ItemName = "Item 2",
                            ItemPrice = 65.87,
                            ItemStatus = "Pending",
                            OrderEntityOrderID = new Guid("d43866ae-89c1-445a-bcac-1ac1eebd0cae")
                        }
                     }
                }
            };

            var mockRepo = new Mock<IOrderRepository>();

            //Get all orders
            mockRepo.Setup(x => x.GetOrders()).ReturnsAsync(orders);

            //Get specific order (by Id)
            mockRepo.Setup(x => x.GetOrderById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                return orders.First(id => id.OrderID == guid);
            });

            //Update Existing order (object)
            mockRepo.Setup(x => x.UpdateOrder(It.IsAny<OrderEntity>())).ReturnsAsync((OrderEntity order) =>
            {
                return order;
            });

            //Delete Order
            mockRepo.Setup(x => x.DeleteOrderById(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                var item = orders.First(id => id.OrderID == guid);
                orders.Remove(item);
                return item;
            });

            return mockRepo;
        }
    }
}
