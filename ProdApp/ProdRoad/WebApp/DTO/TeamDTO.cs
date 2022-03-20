using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class TeamDTO : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(20)] public string Code { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<UserTeam>? UserTeams { get; set; }
    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
}