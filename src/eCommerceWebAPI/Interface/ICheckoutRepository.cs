using eCommerceWebAPI.Entities;

namespace eCommerceWebAPI.Interface
{
    public interface ICheckoutRepository
    {
        public Task<Guid> Checkout(Guid userId);
    }
}
