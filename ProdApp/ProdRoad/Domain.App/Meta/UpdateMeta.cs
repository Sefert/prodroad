

using Domain.App.Identity;
using Domain.Base;

namespace Domain.App.Meta;

public abstract class UpdateMeta : BaseEntity
{
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid UpdatedId { get; set; }
    public AppUser? UpdatedBy { get; set; }
}