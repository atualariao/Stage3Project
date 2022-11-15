using S3E1.Entities;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace S3E1.Contracts
{
    public interface ICheckoutRepository
    {
        public Task<OrderEntity> Checkout(OrderEntity orderEntity);
    }
}
