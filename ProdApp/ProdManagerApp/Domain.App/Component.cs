using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.Base;

namespace Domain.App
{
    public class Component : DomainEntityId
    {
        [MaxLength(50)] public string Name { get; set; } = default!;
        [MaxLength(30)] public string Type { get; set; } = default!;
        [MaxLength(30)] public string Unit { get; set; } = default!;

        public ICollection<Supply>? Supplys { get; set; }
        public ICollection<Price>? Prices { get; set; }
        public ICollection<OrderData>? OrderDatas { get; set; }
        public ICollection<Production>? Productions { get; set; }
        public ICollection<UserUnit>? UserUnits { get; set; }
    }
}