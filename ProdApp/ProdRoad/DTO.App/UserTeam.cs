using Domain.Base;
using DTO.App.Identity;

namespace DTO.App;

public class UserTeam : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
}

