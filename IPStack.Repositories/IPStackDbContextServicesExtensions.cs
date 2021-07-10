using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace IPStack.Repositories
{
    public static class IPStackDbContextServicesExtensions
    {
        public static IServiceCollection AddIPStackDbContextServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IPStackDbContext>(options =>
            {
                options.UseSqlServer(
                    configuration.GetConnectionString("IPStackDbContext"),
                    b =>
                    {
                        b.MigrationsAssembly("IPStack.Repositories");
                    });
            });

            return services;
        }
    }
}
