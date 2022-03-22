using Base.Contracts.Domain;

namespace Domain.Base.Meta;

public abstract class PlanMeta : PlanMeta<Guid>, IBaseEntity
{
    
}

public abstract class PlanMeta<TKey> : ModificationMeta<Guid>, IPlanMeta
where TKey: IEquatable<TKey>
{
    public DateTime StartAt { get; set; } = DateTime.UtcNow;
    public DateTime EndAt { get; set; } = DateTime.UtcNow;
    public DateTime RealStartAt { get; set; } = DateTime.UtcNow;
    public DateTime RealEndAt { get; set; } = DateTime.UtcNow;
}