using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnterpriseAi.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        [HttpGet("me")]
        [Authorize]
        public IActionResult Me()
        {
            return Ok(new
            {
                Name = User.Identity?.Name,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }

        [HttpGet("read")]
        [Authorize(Policy = "AppUserOrAdmin")]
        public IActionResult ReadOnly()
        {
            return Ok("Read access granted.");
        }

        [HttpPost("admin")]
        [Authorize(Policy = "AppAdminOnly")]
        public IActionResult AdminOnly()
        {
            return Ok("Admin access granted.");
        }
    }
}
