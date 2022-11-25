using Bogus;
using Moq;
using S3E1.Contracts;
using S3E1.DTO;
using S3E1.Entities;

namespace Test.Moq
{
    public static class MockCartItemEntityRepository
    {
        public static Mock<ICartItemRepository> CartitemRepo()
        {
            var items = new List<CartItemEntity>
            {
                new CartItemEntity
                {
                    ItemID = new Guid("e9d124ab-355a-4b11-8066-39fdccf7d051"),
                    ItemName = "item 1",
                    ItemPrice = 34.56,
                    ItemStatus = "Pending"
                },

                new CartItemEntity
                {
                    ItemID = new Guid("c2eb76e6-1367-4908-903a-0896b35cda77"),
                    ItemName = "Item 2",
                    ItemPrice = 69.69,
                    ItemStatus = "Pending"
                },

                new CartItemEntity
                {
                    ItemID = new Guid("dd4ebf94-0d42-4a2b-a4d2-d69889f495eb"),
                    ItemName = "Item 3",
                    ItemPrice = 32.54,
                    ItemStatus = "Pending"
                }
            };

            var mockRepo = new Mock<ICartItemRepository>();

            //Get all items
            mockRepo.Setup(x => x.GetCartItems()).ReturnsAsync(items);

            //Get specific item (by Id)
            mockRepo.Setup(x => x.GetCartItemEntity(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                return items.First(id => id.ItemID == guid);
            });

            //Update Existing item (object)
            mockRepo.Setup(x => x.Updateitem(It.IsAny<CartItemEntity>())).ReturnsAsync((CartItemEntity cart) =>
            {
                return cart;
            });

            //Create new item
            mockRepo.Setup(x => x.Createitem(It.IsAny<CartItemEntity>())).ReturnsAsync((CartItemEntity cart) =>
            {
                items.Add(cart);
                return cart;
            });

            //Delete Cart Item
            mockRepo.Setup(x => x.DeleteItem(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                var item = items.First(id => id.ItemID == guid);
                items.Remove(item);
                return item;
            });

            return mockRepo;
        }
    }
}

//BOGUS FORMAT


//public static List<item> GetItems()
//{
//    Faker<item> itemsGenerator = new Faker<item>()
//        .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
//        .RuleFor(item => item.ItemName, bogus => bogus.Random.Word())
//        .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double());

//    return itemsGenerator.Generate(5);
//}
//class item
//{
//    public Guid ItemID { get; set; }
//    public string ItemName { get; set; }
//    public double ItemPrice { get; set; }
//    public string ItemStatus { get; set; } = "Pending";
//}