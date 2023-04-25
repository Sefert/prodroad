namespace Public.App.DTO.v1.Identity;

public class JwtResponse
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}