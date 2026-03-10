namespace EnterpriseAi.Api.Models.Domain;

public class RetrievedChunk
{
    public string Id { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SourceFile { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public double? Score { get; set; }
}