using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAi.Api.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new
            {
                status = "Healthy",
                utcTime = DateTime.UtcNow
            });
        }
    }
}
