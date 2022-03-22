using Domain.App.Identity;

namespace Domain.App.Meta;

public class PlanMeta : Domain.Base.Meta.PlanMeta
{
    public Guid CreatedId { get; set; }
    public AppUser? CreatedBy { get; set; }
    
    public Guid UpdatedId { get; set; }
    public AppUser? UpdatedBy { get; set; }
}