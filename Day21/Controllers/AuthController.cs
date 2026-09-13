using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public IActionResult Login(LoginRequest request)
    {
        var response = _authService.Login(request);

        if (response is null)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password"
            });
        }

        return Ok(response);
    }
}