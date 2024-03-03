using WebApp.Domain.Identity;

namespace WebApp.Domain;

public class UserTeam : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; } = default!;
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; } = default!;
}