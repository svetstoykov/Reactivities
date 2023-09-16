using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using API.Identity.Policies;
using Application.Common.Utility;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Identity.Models;
using Infrastructure.Pictures.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace API.Common.Extensions;

public static class WebServiceExtensions
{
    public static IServiceCollection AddWebServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers();
        services.AddSignalR();
        services.Configure<CloudinarySettings>(configuration.GetSection(GlobalConstants.Cloudinary));

        return services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddSwagger()
            .AddCorsPolicy()
            .AddCustomFluentValidation()
            .AddIdentity(configuration);
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddIdentityCore<User>()
            .AddEntityFrameworkStores<DataContext>()
            .AddSignInManager<SignInManager<User>>();

        services.AddJwtAuthentication(configuration);

        services.AddAuthorization(cfg =>
        {
            cfg.AddPolicy(GlobalConstants.IsActivityHostPolicy, policy =>
            {
                policy.Requirements.Add(new IsHostRequirement());
            });
        });
            
        services.AddScoped<IAuthorizationHandler, IsHostRequirementHandler>();

        return services;
    }

    private static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[GlobalConstants.TokenKey]));

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(cfg =>
            {
                cfg.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
                cfg.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/comments"))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
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
                policy
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .WithOrigins("http://localhost:3000");
            });
        });
}