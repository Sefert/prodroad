using Domain.App.Identity;
using Domain.Base;
using Domain.Base.Identity;

namespace Domain.App.Meta;

public abstract class ModificationMeta : BaseEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid CreatedId { get; set; }
    public AppUser? CreatedBy { get; set; }
    
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid UpdatedId { get; set; }
    public AppUser? UpdatedBy { get; set; }
}