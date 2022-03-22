using Domain.App.Identity;
using Domain.Base;

namespace Domain.App.Meta;

public class ModificationMeta : Domain.Base.Meta.ModificationMeta
{   
    public Guid CreatedId { get; set; }
    public AppUser? CreatedBy { get; set; }
    
    public Guid UpdatedId { get; set; }
    public AppUser? UpdatedBy { get; set; }
}