using System.ComponentModel.DataAnnotations;

namespace WebApp.DTO.Identity;

public class EditPassword
{
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong email length")]
    public string Email { get; set; }
    
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong password length")]
    public string OldPassword { get; set; }
    
    [StringLength(maximumLength:128,MinimumLength = 5, ErrorMessage = "Wrong password length")]
    public string NewPassword { get; set; }
}