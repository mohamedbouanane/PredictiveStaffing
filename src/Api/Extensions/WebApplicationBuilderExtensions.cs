namespace Api.Extensions;

using Api.Extensions.GraphQL;
using Application;
using Infrastructure;
using Infrastructure.Settings;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using ServiceDefaults.Extensions;
using ServiceDefaults.Extensions.EndPoints;
using ServiceDefaults.Extensions.Security.Cors;
using ServiceDefaults.Extensions.Security.Https;
using ServiceDefaults.Extensions.SerilogLogger;
using ServiceDefaults.Extensions.SettingsFiles;
using ServiceDefaults.Extensions.SwaggerUI;
using YamlDotNet.Serialization;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder ConfigureWebApplicationBuilder(this WebApplicationBuilder builder, AppSettings settings)
    {
        _ = builder.Configuration.AddYamlSettingsFiles(settings: settings);
        _ = builder.Services.AddHttpContextAccessor();
        _ = builder.Logging.ClearProviders();

        Log.Logger = new LoggerConfiguration().Configure
        (
            services: builder.Services,
            configuration: builder.Configuration
        ).CreateLogger();

        _ = builder.Services.AddBaseServices(configuration: builder.Configuration);
        _ = builder.Host.UseSerilog();
        _ = builder.AddHttpsServices();

        return builder;
    }

    private static IServiceCollection AddBaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSerilogServices();
        _ = services.AddMemoryCache();
        _ = services.AddGraphQLConfiguration(configuration: configuration);
        _ = services.AddRestEndpointsConfiguration();
        _ = services.AddSwaggerServices();
        _ = services.AddInfrastructureServices(configuration: configuration);
        _ = services.AddBusinessServices();
        _ = services.AddSecurityServices(configuration: configuration);

        return services;
    }

    private static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var infrastructureSettings = configuration.GetSection<InfrastructureSettings>();

        ArgumentNullException.ThrowIfNull(infrastructureSettings);

        _ = services.AddInfrastructureServices(settings: infrastructureSettings);

        return services;
    }

    private static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        _ = services.AddApplicationServices();

        return services;
    }

    private static IServiceCollection AddSecurityServices(this IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection<CorsSettings>();

        ArgumentNullException.ThrowIfNull(settings);

        _ = services.AddCorsPolicies(settings: settings);

        return services;
    }
}
