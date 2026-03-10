namespace EnterpriseAi.Api.Services.Search;

public interface ISearchIndexService
{
    Task CreateOrUpdateIndexAsync(CancellationToken cancellationToken = default);
}