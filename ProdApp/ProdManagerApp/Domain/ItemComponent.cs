using System;
using Domain.NotMapped;

namespace Domain
{
    public class ItemComponent : BaseIdentity
    {
        public Guid ComponentId { get; set; }
        public Component Component { get; set; } = default!;

        public Guid ItemId { get; set; }
        public Item Item { get; set; } = default!;
    }
}