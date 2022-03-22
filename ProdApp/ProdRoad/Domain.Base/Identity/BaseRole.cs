using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base.Identity;

public abstract class BaseRole : BaseRole<Guid>, IBaseEntity
{
    public BaseRole() : base()
    {
    }

    public BaseRole(string roleName) : base(roleName)
    {
    }    
}

public abstract class BaseRole<TKey> : IdentityRole<TKey> , IBaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public BaseRole(): base()
    {
    }

    public BaseRole(string roleName): base(roleName)
    {
    }
    
}
