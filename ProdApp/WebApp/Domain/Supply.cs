using System;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class Supply : BaseIdentity
    {
        public decimal Quantity { get; set; } = default!;

        public Guid ItemId { get; set; }
        public Item? Item { get; set; }

        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }
        
        public Guid WarehouseId { get; set; }
        public Warehouse Warehouse { get; set; } = default!;
    }
}