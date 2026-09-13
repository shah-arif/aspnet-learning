
public sealed class CurrentUserResponse
{
    public string? UserId { get; set; }

    public string? Username { get; set; }

    public string? Role { get; set; }

    public List<string> Permissions { get; set; } = [];
}