
using System;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    /*TODO: Add reference to user*/
    public class UserNotification : BaseIdentity
    {
        [MaxLength(20)]
        public string Name { get; set; } = default!;
        [MaxLength(20)]
        public string Color { get; set; } = default!;
        public bool Active { get; set; }
        
        public Guid NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; } = default!;
    }
}