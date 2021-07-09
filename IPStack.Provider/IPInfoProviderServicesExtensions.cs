
using IPStack.Adapter;
using IPStack.Adapter.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: CLSCompliant(false)]
namespace IPStack
{
    public static class IPInfoProviderServicesExtensions
    {
        public static IServiceCollection AddIPInfoProviderServices(this IServiceCollection services)
        {
            services.AddHttpClient<IIPInfoProvider, IPInfoProvider>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
