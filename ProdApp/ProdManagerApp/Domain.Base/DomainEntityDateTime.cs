using System;
using System.ComponentModel.DataAnnotations;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityDateTime : DomainEntityDateTime<Guid> , IDomainEntityDateTime
    {

    }
    public abstract class DomainEntityDateTime<TKey> : DomainEntityId<TKey>, IDomainEntityDate<TKey>, IDomainEntityTime<TKey>
        where TKey : IEquatable<TKey>
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}