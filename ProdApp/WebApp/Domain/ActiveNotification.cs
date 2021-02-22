using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DateTime = Domain.NotMapped.DateTime;


namespace Domain
{
    /*TODO: Add reference to user*/
    public class ActiveNotification : DateTime
    {
        [MaxLength(50)]
        public string Head { get; set; } = default!;
        [MaxLength(300)]
        public string Info { get; set; } = default!;

        public Guid OrderId { get; set; }
        public Order? Order { get; set; }
        
        public Guid SupplyId { get; set; }
        public Supply? Supply { get; set; }

        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
        
        public Guid MasterNotificationId { get; set; }
        public ActiveNotification? MasterNotification { get; set; }
    }
}