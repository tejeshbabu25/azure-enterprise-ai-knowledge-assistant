using EnterpriseAi.Api.Models.Domain;

namespace EnterpriseAi.Api.Services.Common;

public interface IPromptBuilder
{
    string BuildGroundedPrompt(string question, IReadOnlyCollection<RetrievedChunk> chunks);
}