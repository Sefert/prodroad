namespace WebApp.DTO.Identity;

public class RefreshTokenModel
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
}