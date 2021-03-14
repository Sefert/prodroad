using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Team : DomainEntityDate, IDomainEntityId
    {
        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(20)] public string Code { get; set; } = default!;

        public ICollection<UserTeam>? UserTeams { get; set; }
        
    }
}