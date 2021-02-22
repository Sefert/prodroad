using System;
using DateTime = Domain.NotMapped.DateTime;

namespace Domain
{
    public class Production : DateTime
    {
        public decimal Quantity { get; set; }
        public bool UsedProduced { get; set; }

        public Guid ProductionMetaId { get; set; }
        public ProductionMeta ProductionMeta { get; set; } = default!;
        
        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }
        
        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
    }
}