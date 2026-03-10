using EnterpriseAi.Api.Models.Responses;

namespace EnterpriseAi.Api.Services.Orchestration;

public interface IKnowledgeAssistantService
{
    Task<AskResponse> AskAsync(string question, CancellationToken cancellationToken = default);
}