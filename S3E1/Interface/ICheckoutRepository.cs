using S3E1.Entities;

namespace S3E1.Interface
{
    public interface ICheckoutRepository
    {
        public Task<Order> Checkout(Order orders);
    }
}
