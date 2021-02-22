using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class NotificationType : BaseIdentity
    {
        [MaxLength(20)] public string Type { get; set; } = default!;
        
        public ICollection<UserNotification>? UserNotifications { get; set; }
    }
}