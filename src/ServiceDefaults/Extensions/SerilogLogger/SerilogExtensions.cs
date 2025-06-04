namespace ServiceDefaults.Extensions.SerilogLogger;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Builder;
using Serilog.Formatting.Compact;
using System.Globalization;
using System.Reflection;
using Extensions.Common;
using Serilog;

public static class SerilogExtensions
{
    public static IHostBuilder UseSerilogConfiguration(this IHostBuilder host, IServiceCollection services)
    {
        return host.UseSerilog((hostContext, loggerConfiguration) => _ = loggerConfiguration.Configure(services, hostContext.Configuration));
    }

    public static LoggerConfiguration Configure(this LoggerConfiguration loggerConfiguration, IServiceCollection services, IConfiguration configuration)
    {
        var settings = configuration.GetSection<SerilogSettings>();
        ArgumentNullException.ThrowIfNull(settings);

        var provider = services.BuildServiceProvider();
        var logFilePath = BuildLogFilePath(settings);
        var fileInfo = new FileInfo(logFilePath)
           .CreateDirectory();

        var assembly = Assembly.GetEntryAssembly();

        return loggerConfiguration
            .ReadFrom.Configuration(configuration)
            .WriteTo.Async(
                configure => configure.Console(
                    outputTemplate: settings.ConsoleTemplate,
                    formatProvider: CultureInfo.InvariantCulture),
                bufferSize: settings.LogsBufferSize,
                blockWhenFull: settings.BlockWhenLogsBufferFull
            )
            .WriteTo.Async(
                configure => configure.File(
                    path: fileInfo.FullName,
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: settings.FileSizeLimitBytes,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: settings.RetainedFileCountLimit,
                    formatter: new CompactJsonFormatter()),
                bufferSize: settings.LogsBufferSize,
                blockWhenFull: settings.BlockWhenLogsBufferFull
            );
    }

    public static IApplicationBuilder UseSerilogConfiguration(this IApplicationBuilder app)
    {
        _ = app.UseSerilogRequestLogging();

        return app;
    }

    public static IServiceCollection AddSerilogServices(this IServiceCollection services)
    {
        _ = services.AddSerilog();

        return services;
    }

    private static string BuildLogFilePath(SerilogSettings parrams)
    {
        var currentProjectPath = Directory.GetCurrentDirectory();
        return System.IO.Path.Combine(currentProjectPath[..2], parrams.StartDir, currentProjectPath[3..], parrams.FileName);
    }
}
