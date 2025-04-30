namespace ServiceDefaults.Extensions.Security.Cors;

public sealed class CorsSettings
{
    public string PolicyName { get; set; }
    public string[] AllowedOrigins { get; set; }
    public string[] AllowedHeaders { get; set; }
    public string[] AllowedMethods { get; set; }
}
