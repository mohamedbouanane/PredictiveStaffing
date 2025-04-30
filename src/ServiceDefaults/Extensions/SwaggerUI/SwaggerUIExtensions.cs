namespace ServiceDefaults.Extensions.SwaggerUI;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder;
using ServiceDefaults.Extensions;
using Asp.Versioning.ApiExplorer;
using Extensions.Versioning;

public static class SwaggerUIExtensions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        _ = services.AddEndpointsApiExplorer();
        var provider = services.BuildServiceProvider();
        var configuration = provider.GetRequiredService<IConfiguration>();

        var swaggerSettings = configuration.GetSection<SwaggerUISettings>();
        var versioningSetting = configuration.GetSection<VersionSettings>("DefaultApiVersionSettings");

        _ = services.AddVersioningConfiguration(versioningSetting);

        _ = services.AddSwaggerGen(options =>
        {
            _ = options.AddDocumentation();
        });

        return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
        var settings = app.GetSection<SwaggerUISettings>();
        ArgumentNullException.ThrowIfNull(settings);

        if (settings.ActiveSwaggerUI)
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI(options =>
            {
                var apiVersions = provider?.ApiVersionDescriptions.Count ?? 0;
                for (var i = apiVersions - 1; i >= 0; i--)
                {
                    var description = provider.ApiVersionDescriptions[i];
                    options.SwaggerEndpoint(
                        url: $"/swagger/{description.GroupName}/swagger.json",
                        name: description.GroupName.ToUpperInvariant());
                }
            });
        }

        return app;
    }

    private static SwaggerGenOptions AddDocumentation(this SwaggerGenOptions options)
    {
        var xmlDocFileName = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
        if (File.Exists(xmlDocFileName))
        {
            options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlDocFileName), includeControllerXmlComments: true);
            options.DocInclusionPredicate((name, api) => true);
        }

        return options;
    }
}
