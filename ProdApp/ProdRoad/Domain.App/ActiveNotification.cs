using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App;

public class ActiveNotification : ModificationMeta
{
    public Guid ProcessId { get; set; }
    public Process? Process { get; set; }
    
    public Guid UserNotificationId { get; set; }
    public UserNotification UserNotification { get; set; } = default!;

    public Guid? TeamId { get; set; }
    public Team? Team { get; set; }

    [MaxLength(50)] public string Head { get; set; } = default!;
    [MaxLength(200)] public string Info { get; set; } = default!; 
    public bool Active{ get; set; } = default!;
}