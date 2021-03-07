using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Warehouse : DomainEntityId
    {
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [MaxLength(50)]
        public string Address { get; set; } = default!;

        public ICollection<Supply>? Supplys { get; set; }
        
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}