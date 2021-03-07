using System;
using Domain.Base;

namespace Domain.App
{
    public class ItemComponent : DomainEntityId
    {
        public Guid ComponentId { get; set; }
        public Component Component { get; set; } = default!;

        public Guid ItemId { get; set; }
        public Item Item { get; set; } = default!;
    }
}