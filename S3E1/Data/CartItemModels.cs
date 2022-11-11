//using System;
//using S3E1.Entities;

//namespace S3E1.Data
//{
//    public class CartItemModels
//    {
//        private readonly List<CartItemEntity>? _items;

//        public CartItemModels(List<CartItemEntity>? items) => _items = items;

//        public CartItemModels()
//        {
//            Random rndNum = new();
//            _items = new List<CartItemEntity>()
//            {
//                //new CartItemEntity { ItemID = Guid.NewGuid(), ItemName = "Item 1", ItemPrice = rndNum.NextDouble() * (100.0 - 1.0) + 1.0},
//                //new CartItemEntity { ItemID = Guid.NewGuid(), ItemName = "Item 2", ItemPrice = rndNum.NextDouble() * (100.0 - 1.0) + 1.0},
//            };
//        }

//        public async Task Additem(CartItemEntity cartItem)
//        {
//            _items.Add(cartItem);
//            await Task.CompletedTask;
//        }

//        public async Task<List<CartItemEntity>> GetAllItems() => await Task.FromResult(_items);

//        public async Task<CartItemEntity> GetProductById(Guid guid) =>
//            await Task.FromResult(_items.Single(p => p.ItemID == guid));
//    }
//}
