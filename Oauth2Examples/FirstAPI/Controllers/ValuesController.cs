using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FirstAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ValuesController : ControllerBase
{
    private readonly ILogger<ValuesController> _logger;

    public ValuesController(ILogger<ValuesController> logger)
    {
        _logger = logger;
    }

    [HttpGet("defaultvalue")]
    public IActionResult Get()
    {
        return Ok("default first value");
    }
}
