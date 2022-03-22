using Domain.App.Identity;

namespace Domain.App.Meta;

public class UpdateMeta : Domain.Base.Meta.UpdateMeta
{
    public Guid UpdatedId { get; set; }
    public AppUser? UpdatedBy { get; set; }
}