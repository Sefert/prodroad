using System;
using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityMetaTime : IDomainEntityMetaTime
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}