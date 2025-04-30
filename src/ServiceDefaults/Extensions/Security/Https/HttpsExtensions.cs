namespace ServiceDefaults.Extensions.Security.Https;

using Microsoft.AspNetCore.Builder;
using ServiceDefaults.Extensions;
using Microsoft.AspNetCore.Http;

public static class HttpsExtensions
{
    public static WebApplicationBuilder AddHttpsServices(this WebApplicationBuilder applicationBuilder)
    {
        var settings = applicationBuilder.Configuration.GetSection<HttpsSettings>();

        ArgumentNullException.ThrowIfNull(settings, nameof(settings));

        _ = applicationBuilder.Services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            options.HttpsPort = settings.HttpsPort;
        });

        return applicationBuilder;
    }

    public static IApplicationBuilder UseHttpsConfiguration(this IApplicationBuilder app)
    {
        _ = app.UseHttpsRedirection();

        return app;
    }
}
