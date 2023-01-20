using eCommerceWebAPI.DTOs;
using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Interface
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
