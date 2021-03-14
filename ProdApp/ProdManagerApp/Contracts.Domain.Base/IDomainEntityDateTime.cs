using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityDateTime : IDomainEntityDateTime<Guid>{}
    public interface IDomainEntityDateTime<TKey> : IDomainEntityDate<TKey>, IDomainEntityTime<TKey>
        where TKey: IEquatable<TKey>
    {
        
    }
}