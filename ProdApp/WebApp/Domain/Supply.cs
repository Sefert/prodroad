using System;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    //TODO: Add reference to user
    public class Supply : BaseIdentity
    {
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [MaxLength(50)]
        public string Address { get; set; } = default!;
        public decimal Quantity { get; set; } = default!;

        public Guid ItemId { get; set; }
        public Item? Item { get; set; }

        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }
    }
}