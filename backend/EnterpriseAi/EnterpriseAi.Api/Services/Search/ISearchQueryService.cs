using EnterpriseAi.Api.Models.Domain;

namespace EnterpriseAi.Api.Services.Search;

public interface ISearchQueryService
{
    Task<List<RetrievedChunk>> SearchAsync(float[] vector, int top, CancellationToken cancellationToken = default);
}