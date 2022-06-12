using System.Reflection;
using Persistence;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class WebServiceExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration config)
        {
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddSwagger()
                .AddDbContextConfig(config)
                .AddCorsPolicy();
        }

        private static IServiceCollection AddSwagger(this IServiceCollection services)
            => services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"});
                });

        private static IServiceCollection AddDbContextConfig(this IServiceCollection services, IConfiguration config)
            => services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            });

        private static IServiceCollection AddCorsPolicy(this IServiceCollection services)
            => services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });
    }
}
