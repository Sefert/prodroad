using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityDate : DomainEntityDate<Guid>, IDomainEntityDate
    {

    }
    
    public abstract class DomainEntityDate<TKey> : DomainEntityId<TKey>, IDomainEntityDate<TKey>
        where TKey : IEquatable<TKey>
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime StartDate { get; set; } = default!;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}