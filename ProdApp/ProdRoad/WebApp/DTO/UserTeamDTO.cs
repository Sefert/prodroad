using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class UserTeamDTO : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
}