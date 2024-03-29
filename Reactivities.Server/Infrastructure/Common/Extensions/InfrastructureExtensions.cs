﻿using System.Reflection;
using Application.Common.Utility;
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
        => services.AddMassTransit(cfg =>
        {
            cfg.UsingRabbitMq((_, options) =>
            {
                options.Host(new Uri(config.GetConnectionString("RabbitMQ")!));
                options.UseRawJsonDeserializer(isDefault: true);
                options.UseRawJsonSerializer(isDefault: true);
            });
        });
}