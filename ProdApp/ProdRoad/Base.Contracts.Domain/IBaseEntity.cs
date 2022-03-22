namespace Base.Contracts.Domain;

/// <summary>
/// default Guid based Domain Entity interface
/// </summary>
public interface IBaseEntity : IBaseEntity<Guid>
{
}

/// <summary>
/// Universal Domain Entity interface based on generic PK type
/// </summary>
/// <typeparam name="TKey">Type for primary key</typeparam>
public interface IBaseEntity<TKey>
    where TKey: IEquatable<TKey>
{
    public TKey Id { get; set; }
}
