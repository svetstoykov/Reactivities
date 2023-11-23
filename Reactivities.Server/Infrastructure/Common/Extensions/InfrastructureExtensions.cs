using System.Reflection;
using Application.Common.Utility;
using Application.Messages.Models.Input;
using Infrastructure.Common.Settings;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Common.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration config)
        => services
            .AddDbContext(config)
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddRabbitMq(config)
            .AddConfigurations(config)
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .RegisterServices();

    private static IServiceCollection RegisterServices(this IServiceCollection services)
        => services
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration config)
        => services.AddDbContext<DataContext>(options =>
        {
            options.UseSqlServer(config.GetConnectionString(GlobalConstants.DefaultConnection));
        });

    private static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration config)
        => services
            .Configure<RabbitMqConfiguration>(config.GetSection(nameof(RabbitMqConfiguration)));

    private static IServiceCollection AddRabbitMq(this IServiceCollection services, IConfiguration config)
    {
        var mqConfig = config
            .GetSection(nameof(RabbitMqConfiguration))
            .Get<RabbitMqConfiguration>();
        
        services.AddMassTransit(cfg =>
        {
            cfg.AddRequestClient<GetSenderReceiverConversationRequestModel>(
                mqConfig.GetConversationExchangeName.ToExchangeAddressUri());
            
            cfg.UsingRabbitMq((_, options) =>
            {
                options.Host(new Uri(config.GetConnectionString("RabbitMQ")!));
                options.UseRawJsonDeserializer(isDefault: true);
                options.UseRawJsonSerializer(isDefault: true);
            });
        });

        return services;
    }
}