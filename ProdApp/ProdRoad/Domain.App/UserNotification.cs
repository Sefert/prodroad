using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.App.Identity;
using Domain.Base.Enum;

namespace Domain.App;

public class UserNotification : BaseEntity
{
    public NotificationType NotificationType { get; set; } = default!;
    
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    
    [MaxLength(20)] public string Name { get; set; } = default!;
    [MaxLength(20)] public string Color { get; set; } = default!;
    public bool Active { get; set; } = default!;

    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
}