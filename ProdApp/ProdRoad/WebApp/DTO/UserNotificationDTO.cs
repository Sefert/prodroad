using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;
using Domain.Base.Enum;

namespace WebApp.DTO;

public class UserNotificationDTO : BaseEntity
{
    public NotificationType NotificationType { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(20)] public string Name { get; set; } = default!;
    [MaxLength(20)] public string Color { get; set; } = default!;
    public bool Active { get; set; }

    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
}