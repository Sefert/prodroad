using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityTime : IDomainEntityTime<Guid>{}
    public interface IDomainEntityTime<TKey> : IDomainEntityId<TKey>, IDomainEntityMetaTime
        where TKey: IEquatable<TKey>
    {
        
    }
}