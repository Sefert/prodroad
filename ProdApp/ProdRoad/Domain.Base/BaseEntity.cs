using Base.Contracts.Domain;

namespace Domain.Base;

public abstract class BaseEntity : BaseEntity<Guid>, IBaseEntity 
{
}

public abstract class BaseEntity<TKey> : IBaseEntity<TKey> 
    where TKey : IEquatable<TKey>
{
    public TKey Id { get; set; } = default!;
}
