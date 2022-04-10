using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Identity;

public class Login
{
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong email length")]
    public string Email { get; set; }
    
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong email length")]
    public string Password { get; set; }
}