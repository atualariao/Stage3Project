using S3E1.DTOs;
using S3E1.Entities;

namespace S3E1.IRepository
{
    public interface ICartItemRepository
    {
        public Task<List<CartItem>> GetCartItems();
        public Task<CartItem> GetCartItemEntity(Guid id);
        public Task<CartItem> Createitem(CartItem cartItems);
        public Task<CartItem> Updateitem(CartItem cartItems);
        public Task<CartItem> DeleteItem(Guid id);
    }
}
