using S3E1.DTO;
using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface ICartItemRepository
    {
        public Task<List<CartItemEntity>> GetCartItems();
        public Task<CartItemEntity> GetCartItemEntity(Guid id);
        public Task<CartItemEntity> Createitem(CartItemEntity cartItems);
        public Task<CartItemEntity> Updateitem(CartItemEntity cartItems);
        public Task<CartItemEntity> DeleteItem(Guid id);
    }
}
