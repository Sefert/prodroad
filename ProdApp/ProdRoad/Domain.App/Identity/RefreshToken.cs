using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App.Identity;

public class RefreshToken : BaseEntity
{
    [StringLength(36, MinimumLength = 36)] 
    public string Token { get; set; } = Guid.NewGuid().ToString();

    //UTC
    public DateTime TokenExpirationDateTime { get; set; } = DateTime.UtcNow.AddDays(7);
    
    [StringLength(36, MinimumLength = 36)] 
    public string? PreviousToken { get; set; }

    //UTC
    public DateTime? PreviousTokenExpirationDateTime { get; set; }
}