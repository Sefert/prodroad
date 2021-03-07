using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class NotificationType : DomainEntityId
    {
        [MaxLength(20)] public string Type { get; set; } = default!;
        
        public ICollection<UserNotification>? UserNotifications { get; set; }
    }
}