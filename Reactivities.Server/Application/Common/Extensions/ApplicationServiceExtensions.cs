using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Common.Extensions;

public static class ApplicationServiceExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
    {
        services
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .Scan(scan => scan
                .FromCallingAssembly()
                .AddClasses()
                .AsMatchingInterface());

        return services;
    }
}