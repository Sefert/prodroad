using Base.Contracts.Domain;
using Domain.Base.Identity;

namespace Domain.Base.Meta;

public abstract class ModificationMeta : ModificationMeta<Guid>, IBaseEntity
{
    
}

public abstract class ModificationMeta<TKey> : BaseEntity<TKey>,  IModificationMeta
where TKey : IEquatable<TKey>
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}