using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;

namespace FirstAPI.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ValuesController : ControllerBase
{
    private readonly ILogger<ValuesController> _logger;
    private readonly IHttpClientFactory httpClientFactory;

    public ValuesController(ILogger<ValuesController> logger, IHttpClientFactory httpClientFactory)
    {
        _logger = logger;
        this.httpClientFactory = httpClientFactory;
    }

    [MyCustomAuthorizationFilter]
    [HttpGet("defaultvalue")]
    public IActionResult Get()
    {
        return Ok("default first value");
    }

    [HttpGet("secondvalue")]
    public async Task<IActionResult> GetSecondValue()
    {
        var httpClient = this.httpClientFactory.CreateClient("secondapi");
        var response = await httpClient.GetAsync("Values/defaultvalue");
        var jsonResponse = await response.Content.ReadAsStringAsync();

        return Ok(jsonResponse);
    }
}
