using System;
using System.Collections.Generic;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserTeam : DomainEntityDate, IDomainEntityId, IDomainAppUser<ApplicationUser>
    {
        public bool MasterTeam { get; set; }
        public bool Accepted { get; set; }

        public ICollection<ProductionMeta>? ProductionMetas { get; set; }

        public Guid AppUserId { get; set; }
        public ApplicationUser AppUser { get; set; } = default!;
        
        public Guid TeamId { get; set; }
        public Team Team { get; set; } = default!;
        
    }
}