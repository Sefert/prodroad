using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityMetaDate : DomainEntityMetaDate<Guid>
    {

    }
    
    public abstract class DomainEntityMetaDate<TKey> : DomainEntityId<TKey>, IDomainEntityMetaDate
        where TKey : IEquatable<TKey>
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}