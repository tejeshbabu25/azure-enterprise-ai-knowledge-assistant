namespace EnterpriseAi.Api.Services.Ingestion;

public class FileReaderService : IFileReaderService
{
    public async Task<Dictionary<string, string>> ReadTextFilesAsync(
        string folderPath,
        CancellationToken cancellationToken = default)
    {
        if (!Directory.Exists(folderPath))
        {
            throw new DirectoryNotFoundException($"Folder not found: {folderPath}");
        }

        var result = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        var files = Directory.GetFiles(folderPath, "*.txt", SearchOption.TopDirectoryOnly);

        foreach (var file in files)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var content = await File.ReadAllTextAsync(file, cancellationToken);
            result[file] = content;
        }

        return result;
    }
}