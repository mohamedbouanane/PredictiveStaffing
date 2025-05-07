namespace ServiceDefaults.Extensions.SwaggerUI;

using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.AspNetCore.Builder;
using ServiceDefaults.Extensions;

public static class SwaggerUIExtensions
{
    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {
        _ = services.AddEndpointsApiExplorer();
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
            _ = app.UseSwagger();
            _ = app.UseSwaggerUI();
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
