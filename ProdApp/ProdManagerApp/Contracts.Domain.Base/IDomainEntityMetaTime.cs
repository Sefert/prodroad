using System;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityMetaTime
    {
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}