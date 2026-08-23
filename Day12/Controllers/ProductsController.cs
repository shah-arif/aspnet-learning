using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    // private readonly IConfiguration _configuration;

    // public ProductsController(IConfiguration configuration)
    // {
    //     _configuration = configuration;
    // }

    private readonly ApplicationSettings _settings;

    public ProductsController(IOptions<ApplicationSettings> options)
    {
        _settings = options.Value;
    }

    [HttpGet("config")]
    public IActionResult GetConfiguration()
    {
        var appName = _settings.Name;
        var appVersion = _settings.Version;
        

        return Ok(new { appName, appVersion });
    }
}