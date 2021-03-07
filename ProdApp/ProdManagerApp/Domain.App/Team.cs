using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Team : DomainEntityDate
    {
        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(20)] public string Code { get; set; } = default!;

        public ICollection<UserTeam>? UserTeams { get; set; }
        
    }
}