namespace EnterpriseAi.Api.Services.Ingestion;

public static class TextChunker
{
    public static List<string> Chunk(string text, int chunkSize = 800, int overlap = 100)
    {
        var chunks = new List<string>();

        if (string.IsNullOrWhiteSpace(text))
        {
            return chunks;
        }

        var normalized = text.Replace("\r\n", "\n").Trim();

        int start = 0;
        while (start < normalized.Length)
        {
            int length = Math.Min(chunkSize, normalized.Length - start);
            var chunk = normalized.Substring(start, length).Trim();

            if (!string.IsNullOrWhiteSpace(chunk))
            {
                chunks.Add(chunk);
            }

            if (start + length >= normalized.Length)
            {
                break;
            }

            start += Math.Max(1, chunkSize - overlap);
        }

        return chunks;
    }
}