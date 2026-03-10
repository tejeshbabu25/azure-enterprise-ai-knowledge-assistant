using EnterpriseAi.Api.Models.Requests;
using EnterpriseAi.Api.Models.Responses;
using EnterpriseAi.Api.Services.Orchestration;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAi.Api.Controllers;

[ApiController]
[Route("api/chat")]
public class ChatController : ControllerBase
{
    private readonly IKnowledgeAssistantService _knowledgeAssistantService;

    public ChatController(IKnowledgeAssistantService knowledgeAssistantService)
    {
        _knowledgeAssistantService = knowledgeAssistantService;
    }

    [HttpPost("ask")]
    public async Task<ActionResult<AskResponse>> Ask(
        [FromBody] AskRequest request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Question))
        {
            return BadRequest("Question is required.");
        }

        var result = await _knowledgeAssistantService.AskAsync(request.Question, cancellationToken);
        return Ok(result);
    }
}