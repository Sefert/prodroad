namespace Contracts.Domain.Base
{
    public interface IDomainEntityMeta
    {
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
    }
}