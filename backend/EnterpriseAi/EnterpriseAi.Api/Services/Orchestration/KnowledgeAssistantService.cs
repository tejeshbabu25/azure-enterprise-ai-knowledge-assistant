using EnterpriseAi.Api.Models.Responses;
using EnterpriseAi.Api.Options;
using EnterpriseAi.Api.Services.Common;
using EnterpriseAi.Api.Services.OpenAi;
using EnterpriseAi.Api.Services.Search;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.Orchestration;

public class KnowledgeAssistantService : IKnowledgeAssistantService
{
    private readonly IEmbeddingService _embeddingService;
    private readonly ISearchQueryService _searchQueryService;
    private readonly IChatCompletionService _chatCompletionService;
    private readonly IPromptBuilder _promptBuilder;
    private readonly AppOptions _appOptions;

    public KnowledgeAssistantService(
        IEmbeddingService embeddingService,
        ISearchQueryService searchQueryService,
        IChatCompletionService chatCompletionService,
        IPromptBuilder promptBuilder,
        IOptions<AppOptions> appOptions)
    {
        _embeddingService = embeddingService;
        _searchQueryService = searchQueryService;
        _chatCompletionService = chatCompletionService;
        _promptBuilder = promptBuilder;
        _appOptions = appOptions.Value;
    }

    public async Task<AskResponse> AskAsync(string question, CancellationToken cancellationToken = default)
    {
        var queryVector = await _embeddingService.GenerateEmbeddingAsync(question, cancellationToken);
        var chunks = await _searchQueryService.SearchAsync(queryVector, _appOptions.TopK, cancellationToken);

        var groundedPrompt = _promptBuilder.BuildGroundedPrompt(question, chunks);
        var answer = await _chatCompletionService.GetAnswerAsync(groundedPrompt, cancellationToken);

        return new AskResponse
        {
            Answer = answer,
            Sources = chunks.Select(c => new SourceItem
            {
                Title = c.Title,
                SourceFile = c.SourceFile,
                Content = c.Content
            }).ToList()
        };
    }
}