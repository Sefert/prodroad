using Domain.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class UserTeam : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public Guid TeamId { get; set; }
    public Team? Team { get; set; }
    
    public bool? Accepted { get; set; }
}

