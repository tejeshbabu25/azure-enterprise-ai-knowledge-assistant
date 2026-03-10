using EnterpriseAi.Api.Models.Search;

namespace EnterpriseAi.Api.Services.Search;

public interface ISearchDocumentService
{
    Task UploadDocumentsAsync(IEnumerable<KnowledgeDocument> documents, CancellationToken cancellationToken = default);
}