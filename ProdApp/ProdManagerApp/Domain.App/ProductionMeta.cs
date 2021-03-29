using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class ProductionMeta : DomainEntityDateTime, IDomainEntityId, IDomainAppUser<AppUser>
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
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }

        public ICollection<Production> Productions { get; set; } = default!;
        public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
    }
}