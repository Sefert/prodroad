using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Warehouse : DomainEntityId, IDomainAppUserId, IDomainAppUser<AppUser>
    {
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [MaxLength(50)]
        public string Address { get; set; } = default!;

        public ICollection<Supply>? Supplys { get; set; }
        
        public Guid AppUserId { get; set; }
        public AppUser? AppUser { get; set; }
    }
}