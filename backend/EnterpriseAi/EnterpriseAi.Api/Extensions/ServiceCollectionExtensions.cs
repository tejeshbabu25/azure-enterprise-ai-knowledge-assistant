using EnterpriseAi.Api.Options;
using EnterpriseAi.Api.Services.Common;
using EnterpriseAi.Api.Services.Ingestion;
using EnterpriseAi.Api.Services.OpenAi;
using EnterpriseAi.Api.Services.Orchestration;
using EnterpriseAi.Api.Services.Search;
using Microsoft.OpenApi;

namespace EnterpriseAi.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<AzureOpenAiOptions>(
            configuration.GetSection(AzureOpenAiOptions.SectionName));

        services.Configure<AzureSearchOptions>(
            configuration.GetSection(AzureSearchOptions.SectionName));

        services.Configure<AppOptions>(
            configuration.GetSection(AppOptions.SectionName));

        services.AddHttpClient();

        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Enterprise AI Knowledge Assistant API",
                Version = "v1"
            });
        });

        services.AddCors(options =>
        {
            options.AddPolicy("angular", policy =>
            {
                policy.WithOrigins("http://localhost:4200")
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });

        services.AddScoped<IEmbeddingService, EmbeddingService>();
        services.AddScoped<IChatCompletionService, ChatCompletionService>();
        services.AddScoped<ISearchIndexService, SearchIndexService>();
        services.AddScoped<ISearchDocumentService, SearchDocumentService>();
        services.AddScoped<ISearchQueryService, SearchQueryService>();
        services.AddScoped<IFileReaderService, FileReaderService>();
        services.AddScoped<IIngestionService, IngestionService>();
        services.AddScoped<IKnowledgeAssistantService, KnowledgeAssistantService>();
        services.AddScoped<IPromptBuilder, PromptBuilder>();

        return services;
    }
}