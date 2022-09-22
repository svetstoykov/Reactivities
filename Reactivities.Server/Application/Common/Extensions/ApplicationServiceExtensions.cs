using System.Reflection;
using EasyNetQ;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Models.Common;

namespace Application.Common.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services
                .AddMediatR(Assembly.GetExecutingAssembly())
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddRabbitMqMessageBus(config)
                .Scan(scan => scan
                    .FromCallingAssembly()
                    .AddClasses()
                    .AsMatchingInterface());

            return services;
        }
        
        
        private static IServiceCollection AddRabbitMqMessageBus(this IServiceCollection services, IConfiguration config)
            => services.AddSingleton(
                RabbitHutch.CreateBus(config.GetConnectionString(GlobalConstants.RabbitMQBus)));
    }
}
