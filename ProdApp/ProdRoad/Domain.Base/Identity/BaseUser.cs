using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace Domain.Base.Identity;

public abstract class BaseUser : BaseUser<Guid>, IBaseEntity
{
    public BaseUser() : base()
    {
    }

    public BaseUser(string userName) : base(userName)
    {
    }
}

public abstract class BaseUser<TKey> : IdentityUser<TKey>, IBaseEntity<TKey>
    where TKey : IEquatable<TKey>
{
    public BaseUser() : base()
    {
    }

    public BaseUser(string userName) : base(userName)
    {
    }
}
