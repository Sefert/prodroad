using System;
using Microsoft.AspNetCore.Identity;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityDate : IDomainEntityDate<Guid>{}
    public interface IDomainEntityDate<TKey> : IDomainEntityId<TKey>, IDomainEntityMetaDate
        where TKey: IEquatable<TKey>
    {
        
    }
}