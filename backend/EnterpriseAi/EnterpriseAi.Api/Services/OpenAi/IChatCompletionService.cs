namespace EnterpriseAi.Api.Services.OpenAi;

public interface IChatCompletionService
{
    Task<string> GetAnswerAsync(string groundedPrompt, CancellationToken cancellationToken = default);
}