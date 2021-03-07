using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityTime : DomainEntityTime<Guid>
    {

    }
    
    public abstract class DomainEntityTime<TKey> : DomainEntityId<TKey>, IDomainEntityTime<TKey>
        where TKey : IEquatable<TKey>
    {
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; } = default!;
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime? EndTime { get; set; }
    }
}