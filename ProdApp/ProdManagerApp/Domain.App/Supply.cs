using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;

namespace Domain.App
{
    public class Supply : DomainEntityId
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Quantity { get; set; }

        public Guid? ItemId { get; set; }
        public Item? Item { get; set; }

        public Guid? ComponentId { get; set; }
        public Component? Component { get; set; }
        
        public Guid WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; } = default!;
    }
}