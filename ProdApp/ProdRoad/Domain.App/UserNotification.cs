using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Domain.App.Identity;
using Domain.Base.Enum;

namespace Domain.App;

public class UserNotification : BaseEntity
{
    public NotificationType NotificationType { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(20)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = default!;
    
    [MaxLength(20)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Color { get; set; } = default!;
    
    public bool Active { get; set; }

    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
}