using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using S3E1.IRepository;
using S3E1.Repository;

namespace S3E1
{
    public class AutofacDependencyInjection : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // Repository Pattern
            builder.RegisterType<CartItemRepository>().As<ICartItemRepository>().InstancePerLifetimeScope();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>().InstancePerLifetimeScope();
            builder.RegisterType<CheckoutRepository>().As<ICheckoutRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            builder.RegisterType<AdminRepository>().As<IAdminRepository>().InstancePerLifetimeScope();

            // MediatR
            builder.RegisterMediatR(typeof(CartItemRepository).Assembly);
            builder.RegisterMediatR(typeof(OrderRepository).Assembly);
            builder.RegisterMediatR(typeof(CheckoutRepository).Assembly);
            builder.RegisterMediatR(typeof(UserRepository).Assembly);

        }
    }
}
