namespace ServiceDefaults.Extensions.Versioning;

using Microsoft.Extensions.DependencyInjection;
using Asp.Versioning;

public static class VersioningExtensions
{
    public static IServiceCollection AddVersioningConfiguration(this IServiceCollection services, VersionSettings defaultApiVersionSettings)
    {
        _ = services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion
            (
                majorVersion: defaultApiVersionSettings.MajorVersion,
                minorVersion: defaultApiVersionSettings.MinorVersion,
                status: defaultApiVersionSettings.Status
            );

        }).AddApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
