using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

[ApiController]
[Route("api/[controller]")]

public class ConfigController : ControllerBase
{
    private readonly ApplicationSettings _application;
    private readonly RestaurantSettings _restaurant;

    public ConfigController(IOptions<ApplicationSettings> application, IOptions<RestaurantSettings> restaurant)
    {
        _application = application.Value;
        _restaurant = restaurant.Value;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Ok(new
        {
            Application = _application,
            Restaurant = _restaurant
        });
    }
}