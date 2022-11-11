using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface ICartItemRepository
    {
        public Task<List<CartItemEntity>> GetCartItems();
        public Task<CartItemEntity> GetCartItemEntity(Guid id);
        public Task<CartItemEntity> Createitem(CartItemEntity itemEntity);
        public Task<CartItemEntity> Updateitem(CartItemEntity itemEntity);
        public Task<CartItemEntity> DeleteItem(Guid id);
    }
}
