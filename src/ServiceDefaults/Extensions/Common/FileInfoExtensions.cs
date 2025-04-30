namespace ServiceDefaults.Extensions.Common;

public static class FileInfoExtensions
{
    public static FileInfo CreateDirectory(this FileInfo fileInfo)
    {
        var directoryName = fileInfo.DirectoryName;

        ArgumentNullException.ThrowIfNullOrEmpty(directoryName);

        if (!Directory.Exists(directoryName))
        {
            _ = Directory.CreateDirectory(path: directoryName);
        }
        return fileInfo;
    }
}
