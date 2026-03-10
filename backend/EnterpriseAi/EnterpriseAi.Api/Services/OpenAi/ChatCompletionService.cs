using System.Text;
using System.Text.Json;
using EnterpriseAi.Api.Options;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.OpenAi;

public class ChatCompletionService : IChatCompletionService
{
    private readonly HttpClient _httpClient;
    private readonly AzureOpenAiOptions _options;

    public ChatCompletionService(HttpClient httpClient, IOptions<AzureOpenAiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<string> GetAnswerAsync(string groundedPrompt, CancellationToken cancellationToken = default)
    {
        var url =
            $"{_options.Endpoint.TrimEnd('/')}/openai/deployments/{_options.ChatDeployment}/chat/completions" +
            $"?api-version={_options.ApiVersion}";

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("api-key", _options.ApiKey);

        var payload = new
        {
            messages = new object[]
            {
                new { role = "system", content = "You answer only from provided enterprise context." },
                new { role = "user", content = groundedPrompt }
            },
            temperature = 0.2,
            max_tokens = 500
        };

        request.Content = new StringContent(
            JsonSerializer.Serialize(payload),
            Encoding.UTF8,
            "application/json");

        var response = await _httpClient.SendAsync(request, cancellationToken);
        var responseText = await response.Content.ReadAsStringAsync(cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Chat completion failed. Status: {(int)response.StatusCode}. Body: {responseText}");
        }

        using var doc = JsonDocument.Parse(responseText);
        return doc.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString() ?? string.Empty;
    }
}