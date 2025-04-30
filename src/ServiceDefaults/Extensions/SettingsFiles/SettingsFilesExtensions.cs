namespace ServiceDefaults.Extensions.SettingsFiles;

using Microsoft.Extensions.Configuration;

public static class SettingsFilesExtensions
{
    public static IConfigurationBuilder AddYamlSettingsFiles(this IConfigurationBuilder configuration, AppSettings settings)
    {
        ArgumentNullException.ThrowIfNull(configuration);
        ArgumentNullException.ThrowIfNull(settings);

        _ = configuration
               .AddYamlFile($"{settings.SettingsFilesPath}/{settings.FileNamePrefix}.{settings.SettingsFilesExtension}", optional: false, reloadOnChange: settings.ReloadBaseSettingsFileOnChange)
               .AddYamlFile($"{settings.SettingsFilesPath}/{settings.FileNamePrefix}.{settings.EnvironmentName}.{settings.SettingsFilesExtension}", optional: false, reloadOnChange: settings.ReloadEnvironmentSettingsFileOnChange);

        return configuration;
    }
}
