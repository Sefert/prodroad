namespace Base.Contracts.Domain;

public interface IDomainAppUser<TUser> : IDomainAppUser<Guid,TUser>
    where TUser : class
{
    
}

public interface IDomainAppUser<TKey, TUser>
    where TKey : IEquatable<TKey>
    where TUser : class
{
    
}
