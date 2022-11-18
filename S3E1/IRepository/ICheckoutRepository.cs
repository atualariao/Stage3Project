using S3E1.DTO;
using S3E1.Entities;

namespace S3E1.Contracts
{
    public interface ICheckoutRepository
    {
        public Task<OrderEntity> Checkout(OrderEntity orders);
    }
}
