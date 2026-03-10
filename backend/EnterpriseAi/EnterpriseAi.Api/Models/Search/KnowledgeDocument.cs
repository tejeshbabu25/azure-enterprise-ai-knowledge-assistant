using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;

namespace EnterpriseAi.Api.Models.Search;

public class KnowledgeDocument
{
    [SimpleField(IsKey = true, IsFilterable = true)]
    public string Id { get; set; } = string.Empty;

    [SearchableField(IsFilterable = true)]
    public string Title { get; set; } = string.Empty;

    [SearchableField]
    public string Content { get; set; } = string.Empty;

    [SimpleField(IsFilterable = true)]
    public string SourceFile { get; set; } = string.Empty;

    [SearchableField(IsFilterable = true)]
    public string Category { get; set; } = string.Empty;

    [VectorSearchField(VectorSearchDimensions = 1536, VectorSearchProfileName = "vector-profile")]
    public float[] ContentVector { get; set; } = Array.Empty<float>();
}