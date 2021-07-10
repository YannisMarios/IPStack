using IPStack.Business.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;

[assembly: CLSCompliant(false)]
namespace IPStack.Business
{
    public static class IPStackBusinessServicesExtensions
    {
        public static IServiceCollection AddIPStackBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IIPDetailsService, IPDetailsService>();

            return services;
        }
    }
}
