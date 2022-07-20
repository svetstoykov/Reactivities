using System.Reflection;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace API.Common.Extensions
{
    public static class WebServiceExtensions
    {
        public static IServiceCollection AddWebServices(this IServiceCollection services)
        {
            services.AddControllers();
            
            return services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddSwagger()
                .AddCorsPolicy()
                .AddCustomFluentValidation();
        }

        private static IServiceCollection AddCustomFluentValidation(this IServiceCollection services)
            => services
                .AddFluentValidation(cfg => cfg.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

        private static IServiceCollection AddSwagger(this IServiceCollection services)
            => services
                .AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo {Title = "API", Version = "v1"});
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
