namespace EnterpriseAi.Api.Options;

public class AppOptions
{
    public const string SectionName = "App";

    public string SampleDataPath { get; set; } = "sample-data";
    public int ChunkSize { get; set; } = 800;
    public int ChunkOverlap { get; set; } = 100;
    public int TopK { get; set; } = 5;

    // text-embedding-3-small commonly uses 1536 dimensions in many examples,
    // but always align this with your actual model output.
    public int EmbeddingDimensions { get; set; } = 1536;
}