using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;
using EnterpriseAi.Api.Models.Domain;
using EnterpriseAi.Api.Models.Search;
using EnterpriseAi.Api.Options;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.Search;

public class SearchQueryService : ISearchQueryService
{
    private readonly AzureSearchOptions _searchOptions;

    public SearchQueryService(IOptions<AzureSearchOptions> searchOptions)
    {
        _searchOptions = searchOptions.Value;
    }

    public async Task<List<RetrievedChunk>> SearchAsync(
        float[] vector,
        int top,
        CancellationToken cancellationToken = default)
    {
        var client = new SearchClient(
            new Uri(_searchOptions.Endpoint),
            _searchOptions.IndexName,
            new AzureKeyCredential(_searchOptions.ApiKey));

        var options = new SearchOptions
        {
            Size = top
        };

        options.VectorSearch = new()
        {
            Queries =
            {
                new VectorizedQuery(vector)
                {
                    KNearestNeighborsCount = top,
                    Fields = { "ContentVector" }
                }
            }
        };

        var response = await client.SearchAsync<KnowledgeDocument>(null, options, cancellationToken);
        var results = new List<RetrievedChunk>();

        await foreach (var result in response.Value.GetResultsAsync().WithCancellation(cancellationToken))
        {
            results.Add(new RetrievedChunk
            {
                Id = result.Document.Id,
                Title = result.Document.Title,
                SourceFile = result.Document.SourceFile,
                Content = result.Document.Content,
                Score = result.Score
            });
        }

        return results;
    }
}