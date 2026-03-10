using System.Text;
using System.Text.Json;
using EnterpriseAi.Api.Options;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Services.OpenAi;

public class EmbeddingService : IEmbeddingService
{
    private readonly HttpClient _httpClient;
    private readonly AzureOpenAiOptions _options;

    public EmbeddingService(HttpClient httpClient, IOptions<AzureOpenAiOptions> options)
    {
        _httpClient = httpClient;
        _options = options.Value;
    }

    public async Task<float[]> GenerateEmbeddingAsync(string text, CancellationToken cancellationToken = default)
    {
        var url =
            $"{_options.Endpoint.TrimEnd('/')}/openai/deployments/{_options.EmbeddingDeployment}/embeddings" +
            $"?api-version={_options.ApiVersion}";

        using var request = new HttpRequestMessage(HttpMethod.Post, url);
        request.Headers.Add("api-key", _options.ApiKey);

        var payload = new
        {
            input = text
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
                $"Embedding call failed. Status: {(int)response.StatusCode}. Body: {responseText}");
        }

        using var doc = JsonDocument.Parse(responseText);
        var vector = doc.RootElement
            .GetProperty("data")[0]
            .GetProperty("embedding")
            .EnumerateArray()
            .Select(e => e.GetSingle())
            .ToArray();

        return vector;
    }
}