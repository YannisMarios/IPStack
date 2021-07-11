using IPStack.Business.Services;
using IPStack.Business.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace IPStack.Business
{
    public static class IPStackBusinessServicesExtensions
    {
        public static IServiceCollection AddIPStackBusinessServices(this IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IIPDetailsService, IPDetailsService>();

            return services;
        }
    }
}
