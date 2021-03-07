using System;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntity : DomainEntity<Guid>
    {
    }

    public abstract class DomainEntity<TKey> : DomainEntityId<TKey>, IDomainEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public string CreatedBy { get; set; } = "system";
        public string UpdatedBy { get; set; } = "system";
    }
}