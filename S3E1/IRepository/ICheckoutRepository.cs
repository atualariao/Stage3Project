using S3E1.Entities;

namespace S3E1.IRepository
{
    public interface ICheckoutRepository
    {
        public Task<OrderEntity> Checkout(OrderEntity orders);
    }
}
