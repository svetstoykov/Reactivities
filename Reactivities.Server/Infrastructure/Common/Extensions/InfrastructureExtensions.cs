using System.Reflection;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddDbContext(config)
                .AddMediatR(Assembly.GetExecutingAssembly());

            return services
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface());
        }


        private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
            => services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });
    }
}
