using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;

var builder = WebApplication.CreateBuilder(args);

// --------------------------------------------------
// 1. Configuration
// --------------------------------------------------

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("Jwt"));

var jwtSettings = builder.Configuration
    .GetSection("Jwt")
    .Get<JwtSettings>()
    ?? throw new InvalidOperationException(
        "JWT settings are missing.");

// --------------------------------------------------
// 2. Database
// --------------------------------------------------

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("JwtHomeworkDatabase");
});

// --------------------------------------------------
// 3. JWT Authentication
// --------------------------------------------------

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            jwtSettings.Key)),

                ValidateIssuer = true,

                ValidIssuer = jwtSettings.Issuer,

                ValidateAudience = true,

                ValidAudience = jwtSettings.Audience,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero,

                NameClaimType = System.Security.Claims.ClaimTypes.Name,

                RoleClaimType = System.Security.Claims.ClaimTypes.Role
            };
    });

// --------------------------------------------------
// 4. Authorization
// --------------------------------------------------

builder.Services.AddAuthorization(options =>
{
    // Admin-only policy.
    options.AddPolicy(
        "AdminOnly",
        policy =>
        {
            policy.RequireRole("Admin");
        });

    // Permission-based policy.
    options.AddPolicy(
        "CanDeleteProduct",
        policy =>
        {
            policy.RequireClaim(
                "permission",
                "products:delete");
        });
});

// --------------------------------------------------
// 5. Application services
// --------------------------------------------------

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();

builder.Services.AddControllers();

// --------------------------------------------------
// 6. Swagger
// --------------------------------------------------

builder.Services.AddEndpointsApiExplorer();


// ... (everything up to Swagger config stays the same) ...

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc(
        "v1",
        new OpenApiInfo
        {
            Title = "JWT Homework API",
            Version = "v1",
            Description =
                "JWT Authentication and Authorization Practice API"
        });

    options.AddSecurityDefinition(
        "Bearer",
        new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description =
                "Enter your JWT token. Example: Bearer eyJhbGci..."
        });

    // AddSecurityRequirement now takes a delegate: Func<OpenApiDocument, OpenApiSecurityRequirement>
    options.AddSecurityRequirement(document =>
    new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecuritySchemeReference("Bearer", document),
            new List<string>()   // <-- was Array.Empty<string>()
        }
    });
});

// --------------------------------------------------
// 7. Build application
// --------------------------------------------------

var app = builder.Build();

// --------------------------------------------------
// 8. Seed database
// --------------------------------------------------

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider
        .GetRequiredService<AppDbContext>();

    await DbSeeder.SeedAsync(dbContext);
}

// --------------------------------------------------
// 9. HTTP pipeline
// --------------------------------------------------

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Authentication must come before authorization.
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();