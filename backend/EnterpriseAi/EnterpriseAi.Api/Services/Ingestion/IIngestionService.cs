namespace EnterpriseAi.Api.Services.Ingestion;

public interface IIngestionService
{
    Task<int> IngestFolderAsync(string folderPath, CancellationToken cancellationToken = default);
}