using ServiceDefaults.Extensions.SettingsFiles;
using Api.Extensions;
using Serilog;

var currentEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
var builder = WebApplication.CreateBuilder(args);

_ = builder.ConfigureWebApplicationBuilder(settings: new AppSettings()
{
    SettingsFilesPath = "./AppSettings",
    FileNamePrefix = "appsettings",
    EnvironmentName = currentEnvironment,
    SettingsFilesExtension = "yaml",
    ReloadBaseSettingsFileOnChange = false,
    ReloadEnvironmentSettingsFileOnChange = true
});

var app = builder.Build();
_ = app.ConfigureApplication();

try
{
    await app.RunAsync();
    return 0;
}
catch (Exception ex)
{
    Log.Fatal(ex, "Oops Unexpected Error !!!!");
    return 1;
}
finally
{
    await Log.CloseAndFlushAsync();
}