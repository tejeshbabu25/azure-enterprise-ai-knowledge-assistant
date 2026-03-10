using EnterpriseAi.Api.Models.Requests;
using EnterpriseAi.Api.Models.Responses;
using EnterpriseAi.Api.Options;
using EnterpriseAi.Api.Services.Ingestion;
using EnterpriseAi.Api.Services.Search;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace EnterpriseAi.Api.Controllers;

[ApiController]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly ISearchIndexService _searchIndexService;
    private readonly IIngestionService _ingestionService;
    private readonly AppOptions _appOptions;
    private readonly IWebHostEnvironment _environment;

    public AdminController(
        ISearchIndexService searchIndexService,
        IIngestionService ingestionService,
        IOptions<AppOptions> appOptions,
        IWebHostEnvironment environment)
    {
        _searchIndexService = searchIndexService;
        _ingestionService = ingestionService;
        _appOptions = appOptions.Value;
        _environment = environment;
    }

    [HttpPost("setup")]
    public async Task<ActionResult<OperationResult>> Setup(CancellationToken cancellationToken)
    {
        await _searchIndexService.CreateOrUpdateIndexAsync(cancellationToken);
        return Ok(OperationResult.Ok("Search index created or updated successfully."));
    }

    [HttpPost("ingest")]
    public async Task<ActionResult<OperationResult>> Ingest(
        [FromBody] IngestRequest? request,
        CancellationToken cancellationToken)
    {
        var folderPath = request?.FolderPath;
        if (string.IsNullOrWhiteSpace(folderPath))
        {
            folderPath = Path.Combine(_environment.ContentRootPath, _appOptions.SampleDataPath);
        }

        var count = await _ingestionService.IngestFolderAsync(folderPath, cancellationToken);

        return Ok(OperationResult.Ok($"Ingested {count} chunk documents from '{folderPath}'."));
    }
}