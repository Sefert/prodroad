using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class BaseEntity : BaseEntity<Guid>, IDomainEntityId
{
}

public abstract class BaseEntity<TKey> : IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; }
}
