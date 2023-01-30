using Autofac.Extensions.DependencyInjection;
using Autofac;
using eCommerceWebAPI.Configurations;

namespace eCommerceWebAPI.Extensions
{
    public static class HostExtensions
    {
        //Autofac
        public static void ConfigureAutofac(this IHostBuilder host) =>
            host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
            .ConfigureContainer<ContainerBuilder>(builder =>
            {
                builder.RegisterModule(new AutofacDependencyInjection());
            });
    }
}
