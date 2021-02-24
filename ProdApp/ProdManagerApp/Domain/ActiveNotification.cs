using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;


namespace Domain
{
    public class ActiveNotification : MetaDateTime
    {
        [MaxLength(50)]
        public string Head { get; set; } = default!;
        [MaxLength(300)]
        public string Info { get; set; } = default!;

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        
        public Guid SupplyId { get; set; }
        public Supply? Supply { get; set; }
        
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
        
        public Guid MasterNotificationId { get; set; }
        public ActiveNotification? MasterNotification { get; set; }
    }
}