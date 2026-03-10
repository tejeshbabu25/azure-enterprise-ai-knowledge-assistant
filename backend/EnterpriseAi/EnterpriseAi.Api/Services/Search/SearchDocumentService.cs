using Azure;
using Azure.Search.Documents;
using EnterpriseAi.Api.Models.Search;
using EnterpriseAi.Api.Options;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.Search;

public class SearchDocumentService : ISearchDocumentService
{
    private readonly AzureSearchOptions _searchOptions;

    public SearchDocumentService(IOptions<AzureSearchOptions> searchOptions)
    {
        _searchOptions = searchOptions.Value;
    }

    public async Task UploadDocumentsAsync(IEnumerable<KnowledgeDocument> documents, CancellationToken cancellationToken = default)
    {
        var client = new SearchClient(
            new Uri(_searchOptions.Endpoint),
            _searchOptions.IndexName,
            new AzureKeyCredential(_searchOptions.ApiKey));

        var list = documents.ToList();
        if (list.Count == 0)
        {
            return;
        }

        await client.UploadDocumentsAsync(list, cancellationToken: cancellationToken);
    }
}