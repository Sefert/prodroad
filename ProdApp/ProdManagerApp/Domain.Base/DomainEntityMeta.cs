using Contracts.Domain.Base;

namespace Domain.Base
{
    public abstract class DomainEntityMeta : IDomainEntityMeta
    {
        public string CreatedBy { get; set; } = "system";
        public string UpdatedBy { get; set; } = "system";
    }
}