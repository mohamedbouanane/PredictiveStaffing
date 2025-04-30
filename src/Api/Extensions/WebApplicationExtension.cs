namespace Api.Extensions;

using ServiceDefaults.Extensions.Security.Cors;
using ServiceDefaults.Extensions.Security.Https;
using ServiceDefaults.Extensions.SerilogLogger;
using ServiceDefaults.Extensions.EndPoints;
using ServiceDefaults.Extensions.SwaggerUI;

public static class WebApplicationExtension
{
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        ArgumentNullException.ThrowIfNull(app);

        //_ = app.UseExceptionsHandlerConfiguration(environment: app.Environment);
        _ = app.UseSwaggerConfiguration();
        _ = app.UseSerilogConfiguration();
        _ = app.UseCorsPoliciesConfiguration();
        _ = app.UseHttpsConfiguration();
        _ = app.UseRestEndpointsConfiguration();
        _ = app.MapRestEndpoints();

        return app;
    }

    private static WebApplication MapRestEndpoints(this WebApplication app)
    {
        _ = app.MapControllers();

        return app;
    }
}
