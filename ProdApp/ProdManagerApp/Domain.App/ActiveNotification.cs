using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;


namespace Domain.App
{
    public class ActiveNotification : DomainEntityDateTime, IDomainEntityId, IDomainAppUser<AppUser>
    {
        [MaxLength(50)]
        public string Head { get; set; } = default!;
        [MaxLength(300)]
        public string Info { get; set; } = default!;

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        
        public Guid SupplyId { get; set; }
        public Supply? Supply { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
        
        public Guid MasterNotificationId { get; set; }
        public ActiveNotification? MasterNotification { get; set; }
    }
}