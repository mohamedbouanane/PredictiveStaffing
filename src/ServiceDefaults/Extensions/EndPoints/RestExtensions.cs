namespace ServiceDefaults.Extensions.EndPoints;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;

public static class RestExtensions
{
    public static IServiceCollection AddRestEndpointsConfiguration(this IServiceCollection services)
    {
        _ = services.AddEndpointsApiExplorer();
        _ = services.AddControllers(options =>
            options.ReturnHttpNotAcceptable = true
        );

        return services;
    }

    public static IApplicationBuilder UseRestEndpointsConfiguration(this IApplicationBuilder app)
    {
        _ = app.UseRouting();

        return app;
    }
}
