using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Meta;

namespace WebApp.DTO;

public class ActiveNotificationDTO : ModificationMeta
{
    public Guid? ProcessId { get; set; }
    public Process? Process { get; set; }
    
    public Guid UserNotificationId { get; set; }
    public UserNotification? UserNotification { get; set; }

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }

    [MaxLength(50)]
    public string Head { get; set; } = default!;
    [MaxLength(200)] public string Info { get; set; } = default!; 
    public bool Active{ get; set; }
}