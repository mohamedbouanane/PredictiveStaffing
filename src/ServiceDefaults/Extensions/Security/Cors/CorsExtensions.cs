namespace ServiceDefaults.Extensions.Security.Cors;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using ServiceDefaults.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection AddCorsPolicies(this IServiceCollection services, CorsSettings settings)
    {
        return services
            .AddCors(options => options.AddPolicy(name: settings.PolicyName,
                builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    //.WithOrigins(settings.AllowedOrigins)
                    //.WithHeaders(settings.AllowedHeaders)
                    //.WithMethods(settings.AllowedMethods)
                    //.SetIsOriginAllowed(allowOrigin => true)
                    //.AllowCredentials()
                )
            );
    }

    public static IApplicationBuilder UseCorsPoliciesConfiguration(this IApplicationBuilder app)
    {
        var settings = app.GetSection<CorsSettings>();

        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        _ = app.UseCors(settings.PolicyName);

        return app;
    }
}
