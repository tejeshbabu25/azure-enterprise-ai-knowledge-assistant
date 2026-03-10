namespace EnterpriseAi.Api.Services.Ingestion;

public interface IFileReaderService
{
    Task<Dictionary<string, string>> ReadTextFilesAsync(string folderPath, CancellationToken cancellationToken = default);
}