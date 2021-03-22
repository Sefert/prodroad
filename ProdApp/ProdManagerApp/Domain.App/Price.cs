
using System;
using System.ComponentModel.DataAnnotations.Schema;
using Contracts.Domain.Base;
using Domain.Base;

namespace Domain.App
{
    public class Price : DomainEntityDate, IDomainEntityId
    {
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public Guid ComponentId { get; set; }
        public Component? Component { get; set; }

        public Guid ItemId { get; set; }
        public Item? Item { get; set; }
    }
}