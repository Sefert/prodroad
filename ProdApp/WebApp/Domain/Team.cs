using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class Team : Date
    {
        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(20)] public string Code { get; set; } = default!;

        public ICollection<UserTeam>? UserTeams { get; set; }
        
    }
}