using System;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserNotification : DomainEntityId
    {
        [MaxLength(20)]
        public string Name { get; set; } = default!;
        [MaxLength(20)]
        public string Color { get; set; } = default!;
        public bool Active { get; set; }
        
        public Guid NotificationTypeId { get; set; }
        public NotificationType NotificationType { get; set; } = default!;
        
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}