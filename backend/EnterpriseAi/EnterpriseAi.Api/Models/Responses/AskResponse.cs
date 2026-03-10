namespace EnterpriseAi.Api.Models.Responses
{
    public class AskResponse
    {
        public string Answer { get; set; } = string.Empty;
        public List<SourceItem> Sources { get; set; } = new();
    }
}
