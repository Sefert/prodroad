using System;
using Contracts.Domain.Base;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class UserUnit : DomainEntityDate, IDomainEntityId
    {
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; } = default!;
        
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
        
        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }
    }
}