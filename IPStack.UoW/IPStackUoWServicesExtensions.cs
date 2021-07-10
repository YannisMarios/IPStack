using IPStack.UoW.Implementation;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace IPStack.UoW
{
    public static class IPStackUoWServicesExtensions
    {
        public static IServiceCollection AddIPStackUoWServices(this IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            return services;
        }
    }
}
