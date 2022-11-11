using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface ICartItemRepository
    {
        public Task<IEnumerable<CartItemEntity>> GetCartItems();
        public Task<CartItemEntity> GetCartItemEntity(Guid id);
        public Task Createitem(CartItemEntity itemEntity);
    }
}
