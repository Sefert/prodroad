
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App.Meta;

public abstract class ModificationMeta : BaseEntity
{
    public DateTime CreatedAt { get; set; }
    
    public Guid CreatedId { get; set; }
    public AppUser CreatedBy { get; set; } = default!;
    
    public DateTime? UpdatedAt { get; set; }
    
    public Guid? UpdatedId { get; set; }
    public AppUser? UpdatedBy { get; set; }
}