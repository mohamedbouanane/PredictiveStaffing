namespace Api.Extensions;

using Microsoft.Extensions.DependencyInjection;
using ServiceDefaults.Extensions.Security.Https;
using ServiceDefaults.Extensions.SerilogLogger;
using ServiceDefaults.Extensions.SettingsFiles;
using ServiceDefaults.Extensions.Security.Cors;
using ServiceDefaults.Extensions.SwaggerUI;
using ServiceDefaults.Extensions.EndPoints;
using Microsoft.AspNetCore.Builder;
using ServiceDefaults.Extensions;
using Infrastructure;
using Api.Mappers;
using Serilog;
using Infrastructure.Settings;

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
        _ = builder.Services.AddInfrastructureServices(configuration: builder.Configuration);

        return builder;
    }

    private static IServiceCollection MapModelsToDtos(this IServiceCollection services)
    {
        _ = services.AddAutoMapper(
            typeof(MappingProfileConsultants),
            typeof(MappingProfileMissions)
        );

        return services;
    }

    private static IServiceCollection AddInfrastructureServices (this IServiceCollection services, IConfiguration configuration)
    {
        var infrastructureSettings = configuration.GetSection<InfrastructureSettings>();

        _ = services.AddInfrastructureServices(settings: infrastructureSettings);

        return services;
    }

    private static IServiceCollection AddBaseServices(this IServiceCollection services, IConfiguration configuration)
    {
        _ = services.AddSerilogServices();
        _ = services.AddMemoryCache();
        _ = services.AddRestEndpointsConfiguration();
        _ = services.AddSwaggerServices();
        _ = services.AddSecurityServices(configuration: configuration);
        //_ = services.AddExceptionServices();

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
