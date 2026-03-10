namespace EnterpriseAi.Api.Models.Domain
{
    public class ChunkedDocument
    {
        public string Id { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string SourceFile { get; set; } = string.Empty;
        public string Category { get; set; } = "general";
        public string Content { get; set; } = string.Empty;
    }
}
