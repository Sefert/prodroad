using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Identity;

public class Register
{
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong email length")]
    public string Email { get; set; }
    
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong password length")]
    public string Password { get; set; }
}