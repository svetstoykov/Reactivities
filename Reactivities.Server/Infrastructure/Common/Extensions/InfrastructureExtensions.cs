using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface());
        }
    }
}
