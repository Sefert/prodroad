using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App;

public class Team : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(20)] public string Code { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<UserTeam>? UserTeams { get; set; }
    public ICollection<ActiveNotification>? ActiveNotifications { get; set; }
}