using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Interface
{
    public interface ICheckoutRepository
    {
        public Task<Order> Checkout(Order orders);
    }
}
