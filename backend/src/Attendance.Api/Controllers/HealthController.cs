using Microsoft.AspNetCore.Mvc;

namespace Attendance.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            status = "Healthy",
            timestamp = DateTimeOffset.UtcNow
        });
    }
}
