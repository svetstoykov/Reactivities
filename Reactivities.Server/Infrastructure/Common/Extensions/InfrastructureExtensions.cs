using Infrastructure.Photos.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Common;

namespace Infrastructure.Common.Extensions
{
    public static class InfrastructureExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        {
            return services
                .Configure<CloudinarySettings>(config.GetSection(GlobalConstants.Cloudinary));
        }
    }
}
