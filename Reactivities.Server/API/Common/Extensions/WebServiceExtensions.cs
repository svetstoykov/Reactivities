using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Persistence;

namespace API.Common.Extensions
{
    public static class WebServiceExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddControllers();

            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddSwagger()
                .AddFluentValidation()
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
