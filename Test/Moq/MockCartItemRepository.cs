using Bogus;
using Moq;
using S3E1.Entities;
using S3E1.Interface;

namespace Test.Moq
{
    public class MockCartItemRepository
    {
        public static List<CartItem> GenerateItems()
        {
            Faker<CartItem> itemsGenerator = new Faker<CartItem>()
                .RuleFor(item => item.ItemID, bogus => bogus.Random.Guid())
                .RuleFor(item => item.ItemName, bogus => bogus.Commerce.ProductName())
                .RuleFor(item => item.ItemPrice, bogus => bogus.Random.Double(1.0, 100.0));

            return itemsGenerator.Generate(4);
        }
        public static Mock<ICartItemRepository> CartitemRepo()
        {
            var items = GenerateItems();


            var mockRepo = new Mock<ICartItemRepository>();

            //Get all items
            mockRepo.Setup(x => x.GetCartItems()).ReturnsAsync(items);

            //Get specific item (by Id)
            mockRepo.Setup(x => x.GetCartItemEntity(It.IsAny<Guid>())).ReturnsAsync((Guid guid) =>
            {
                return items.First(id => id.ItemID == guid);
            });

            //Update Existing item (object)
            mockRepo.Setup(x => x.Updateitem(It.IsAny<CartItem>())).ReturnsAsync((CartItem cart) =>
            {
                return cart;
            });

            //Create new item
            mockRepo.Setup(x => x.Createitem(It.IsAny<CartItem>())).ReturnsAsync((CartItem cart) =>
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


