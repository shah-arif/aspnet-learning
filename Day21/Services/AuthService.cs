
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

public class AuthService : IAuthService
{
    private readonly AppDbContext _db;
    private readonly IConfiguration _configuration;

    public AuthService(
        AppDbContext db,
        IConfiguration configuration)
    {
        _db = db;
        _configuration = configuration;
    }

    public LoginResponse? Login(LoginRequest request)
    {
        var user = _db.Users.FirstOrDefault(x =>
            x.Username == request.Username &&
            x.Password == request.Password);

        if (user is null)
        {
            return null;
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(
                _configuration["Jwt:Key"]!
            )
        );

        var credentials = new SigningCredentials(
            key,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(
                double.Parse(_configuration["Jwt:ExpiryMinutes"]!)
            ),
            signingCredentials: credentials
        );

        return new LoginResponse
        {
            AccessToken = new JwtSecurityTokenHandler()
                .WriteToken(token)
        };
    }
}