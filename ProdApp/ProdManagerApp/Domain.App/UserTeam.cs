using System;
using System.Collections.Generic;
using Domain.Base;

namespace Domain.App
{
    public class UserTeam : DomainEntityDate
    {
        public bool MasterTeam { get; set; }
        public bool Accepted { get; set; }

        public ICollection<ProductionMeta>? ProductionMetas { get; set; }

        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; } = default!;
        
        public Guid TeamId { get; set; }
        public Team Team { get; set; } = default!;
        
    }
}