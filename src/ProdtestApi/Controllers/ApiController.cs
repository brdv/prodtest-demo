using Microsoft.AspNetCore.Mvc;

namespace ProdtestApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ApiController : ControllerBase
{
    [HttpGet("health")]
    public IActionResult HealthCheck()
    {
        return Ok("{\"health\": \"ok\" }");
    }
}