using System;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Production : DomainEntityDateTime, IDomainEntityId
    {
        [Column(TypeName = "decimal(18,2)")]
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