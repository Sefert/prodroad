
using System;
using Domain.NotMapped;

namespace Domain
{
    public class Price : Date
    {
        public decimal Amount { get; set; }

        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }

        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
    }
}