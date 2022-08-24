using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence.Common.Extensions
{
    public static class PersistenceExtensions
    {
        public static IServiceCollection AddPersistenceServices(
            this IServiceCollection services,
            IConfiguration config)
        {
            services
                .AddDbContext(config)
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface());

            return services;
        }

    }
}
