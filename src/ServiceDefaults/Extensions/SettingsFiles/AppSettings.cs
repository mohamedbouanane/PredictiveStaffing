namespace ServiceDefaults.Extensions.SettingsFiles;

public sealed class AppSettings
{
    public string EnvironmentName { get; set; }
    public string FileNamePrefix { get; set; }
    public string SettingsFilesPath { get; set; }
    public string SettingsFilesExtension { get; set; }
    public bool ReloadBaseSettingsFileOnChange { get; set; }
    public bool ReloadEnvironmentSettingsFileOnChange { get; set; }
}
