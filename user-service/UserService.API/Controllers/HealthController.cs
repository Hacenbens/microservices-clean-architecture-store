using Microsoft.AspNetCore.Mvc;
using UserService.Infrastructure.Persistance;

namespace UserService.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    [HttpGet("full")]
    public async Task<IActionResult> FullCheck([FromServices] UserDbContext dbContext)
    {
        try
        {
            await dbContext.Database.CanConnectAsync();
            return Ok(new { status = "Healthy", database = "OK" });
        }
        catch
        {
            return StatusCode(503, new { status = "Unhealthy", database = "Failed" });
        }
    }
}