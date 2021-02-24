using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class ProductionMeta : MetaDateTime
    {
        public string Line { get; set; } = default!;
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? RealStartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? RealEndDate { get; set; }

        public Guid SupplyId { get; set; }
        public Supply Supply { get; set; } = default!;
        
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }

        public ICollection<Production> Productions { get; set; } = default!;
        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
    }
}