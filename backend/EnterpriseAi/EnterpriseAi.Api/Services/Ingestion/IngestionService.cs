using EnterpriseAi.Api.Models.Domain;
using EnterpriseAi.Api.Models.Search;
using EnterpriseAi.Api.Options;
using EnterpriseAi.Api.Services.OpenAi;
using EnterpriseAi.Api.Services.Search;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.Ingestion;

public class IngestionService : IIngestionService
{
    private readonly IFileReaderService _fileReaderService;
    private readonly IEmbeddingService _embeddingService;
    private readonly ISearchDocumentService _searchDocumentService;
    private readonly AppOptions _appOptions;

    public IngestionService(
        IFileReaderService fileReaderService,
        IEmbeddingService embeddingService,
        ISearchDocumentService searchDocumentService,
        IOptions<AppOptions> appOptions)
    {
        _fileReaderService = fileReaderService;
        _embeddingService = embeddingService;
        _searchDocumentService = searchDocumentService;
        _appOptions = appOptions.Value;
    }

    public async Task<int> IngestFolderAsync(string folderPath, CancellationToken cancellationToken = default)
    {
        var files = await _fileReaderService.ReadTextFilesAsync(folderPath, cancellationToken);
        var uploadBatch = new List<KnowledgeDocument>();

        foreach (var kvp in files)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var fullPath = kvp.Key;
            var content = kvp.Value;

            var title = Path.GetFileNameWithoutExtension(fullPath);
            var fileName = Path.GetFileName(fullPath);

            var chunks = TextChunker.Chunk(content, _appOptions.ChunkSize, _appOptions.ChunkOverlap);

            for (int i = 0; i < chunks.Count; i++)
            {
                var chunk = chunks[i];
                var vector = await _embeddingService.GenerateEmbeddingAsync(chunk, cancellationToken);

                uploadBatch.Add(new KnowledgeDocument
                {
                    Id = $"{title}-{i}",
                    Title = title,
                    SourceFile = fileName,
                    Category = "general",
                    Content = chunk,
                    ContentVector = vector
                });
            }
        }

        await _searchDocumentService.UploadDocumentsAsync(uploadBatch, cancellationToken);
        return uploadBatch.Count;
    }
}