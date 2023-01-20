using Autofac;
using MediatR.Extensions.Autofac.DependencyInjection;
using eCommerceWebAPI.Interface;
using eCommerceWebAPI.Repository;

namespace eCommerceWebAPI.Configurations
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
