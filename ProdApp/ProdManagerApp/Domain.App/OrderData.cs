using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class OrderData : DomainEntityId
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; } = default!;
        [Column(TypeName = "decimal(18,2)")]
        public decimal Total { get; set; } = default!;

        public Guid OrderId { get; set; }
        public Order Order { get; set; } = default!;

        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }
        
        public Guid SupplyId { get; set; }
        public Supply Supply { get; set; } = default!;

        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
    }
}