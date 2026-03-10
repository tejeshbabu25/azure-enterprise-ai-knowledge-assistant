using Azure;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using EnterpriseAi.Api.Models.Search;
using EnterpriseAi.Api.Options;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.Search;

public class SearchIndexService : ISearchIndexService
{
    private readonly AzureSearchOptions _searchOptions;

    public SearchIndexService(IOptions<AzureSearchOptions> searchOptions)
    {
        _searchOptions = searchOptions.Value;
    }

    public async Task CreateOrUpdateIndexAsync(CancellationToken cancellationToken = default)
    {
        var client = new SearchIndexClient(
            new Uri(_searchOptions.Endpoint),
            new AzureKeyCredential(_searchOptions.ApiKey));

        var fields = new FieldBuilder().Build(typeof(KnowledgeDocument));

        var vectorSearch = new VectorSearch
        {
            Profiles =
            {
                new VectorSearchProfile("vector-profile", "hnsw-config")
            },
            Algorithms =
            {
                new HnswAlgorithmConfiguration("hnsw-config")
            }
        };

        var index = new SearchIndex(_searchOptions.IndexName, fields)
        {
            VectorSearch = vectorSearch
        };

        await client.CreateOrUpdateIndexAsync(index, cancellationToken: cancellationToken);
    }
}