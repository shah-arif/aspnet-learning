using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public sealed class AuthController : ControllerBase
{
    private readonly AppDbContext _dbContext;

    private readonly IJwtTokenService _jwtTokenService;

    private readonly PasswordHasher<AppUser>
        _passwordHasher = new();

    public AuthController(
        AppDbContext dbContext,
        IJwtTokenService jwtTokenService)
    {
        _dbContext = dbContext;

        _jwtTokenService = jwtTokenService;
    }

    // --------------------------------------------------
    // POST: api/auth/login
    // --------------------------------------------------

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<LoginResponse>> Login(
        [FromBody] LoginRequest request)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(
                x => x.Username == request.Username);

        if (user is null || !user.IsActive)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        var passwordResult = _passwordHasher.VerifyHashedPassword(
            user,
            user.PasswordHash,
            request.Password);

        if (passwordResult == PasswordVerificationResult.Failed)
        {
            return Unauthorized(new
            {
                message = "Invalid username or password."
            });
        }

        var token = _jwtTokenService.GenerateToken(user);

        return Ok(new LoginResponse
        {
            AccessToken = token,
            TokenType = "Bearer",
            ExpiresIn = 30 * 60,
            Username = user.Username,
            Role = user.Role
        });
    }

    // --------------------------------------------------
    // GET: api/auth/me
    // --------------------------------------------------

    [HttpGet("me")]
    [Authorize]
    public ActionResult<CurrentUserResponse> GetCurrentUser()
    {
        var userId = User.FindFirstValue(
            ClaimTypes.NameIdentifier);

        var username = User.FindFirstValue(
            ClaimTypes.Name);

        var role = User.FindFirstValue(
            ClaimTypes.Role);

        var permissions = User
            .FindAll("permission")
            .Select(x => x.Value)
            .Distinct()
            .ToList();

        return Ok(new CurrentUserResponse
        {
            UserId = userId,
            Username = username,
            Role = role,
            Permissions = permissions
        });
    }
}