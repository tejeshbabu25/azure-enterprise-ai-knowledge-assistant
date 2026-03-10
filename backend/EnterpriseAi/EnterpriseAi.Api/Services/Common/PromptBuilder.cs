using EnterpriseAi.Api.Models.Domain;

namespace EnterpriseAi.Api.Services.Common;

public class PromptBuilder : IPromptBuilder
{
    public string BuildGroundedPrompt(string question, IReadOnlyCollection<RetrievedChunk> chunks)
    {
        var context = string.Join(
            "\n\n---\n\n",
            chunks.Select(c =>
                $"SourceFile: {c.SourceFile}\nTitle: {c.Title}\nContent: {c.Content}"));

        return $"""
You are an enterprise knowledge assistant.

Rules:
1. Answer only from the provided context.
2. If the answer isn't in the context, say: "I don't know based on the available documents."
3. Keep the answer concise and business-friendly.
4. Include source file names at the end.

Context:
{context}

Question:
{question}
""";
    }
}