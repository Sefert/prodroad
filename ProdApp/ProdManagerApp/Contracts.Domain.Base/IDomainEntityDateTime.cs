using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityDateTime : IDomainEntityDateTime<Guid>{}
    public interface IDomainEntityDateTime<TKey> : IDomainEntityId<TKey>, IDomainEntityMetaDate, IDomainEntityMetaTime
        where TKey: IEquatable<TKey>
    {
        
    }
}