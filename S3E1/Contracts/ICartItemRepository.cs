using S3E1.DTO;
using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface ICartItemRepository
    {
        public Task<List<CartItems>> GetCartItems();
        public Task<CartItemEntity> GetCartItemEntity(Guid id);
        public Task<CartItems> Createitem(CartItems cartItems);
        public Task<CartItems> Updateitem(CartItems cartItems);
        public Task<CartItemEntity> DeleteItem(Guid id);
    }
}
