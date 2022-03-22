using Base.Contracts.Domain;
using Domain.Base.Identity;

namespace Domain.Base.Meta;

public abstract class UpdateMeta : UpdateMeta<Guid>,  IBaseEntity
{
    
}
public abstract class UpdateMeta<TKey>: BaseEntity<TKey>, IUpdateMeta
where TKey : IEquatable<TKey>
{
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}