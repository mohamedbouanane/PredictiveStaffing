namespace ServiceDefaults.Extensions.SerilogLogger;

public sealed class SerilogSettings
{
    public string StartDir { get; set; }
    public string FileName { get; set; }

    public int LogsBufferSize { get; set; }
    public bool BlockWhenLogsBufferFull { get; set; }

    public string ConsoleTemplate { get; set; }
    public string ApplicationName { get; set; }
    public long FileSizeLimitBytes { get; set; }
    public int RetainedFileCountLimit { get; set; }
}
