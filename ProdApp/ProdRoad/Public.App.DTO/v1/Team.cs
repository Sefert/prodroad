using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Public.App.DTO.v1.Identity;

namespace Public.App.DTO.v1;

public class Team : BaseEntity
{
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(20)] public string Code { get; set; } = default!;
    
    public bool IsPublic { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<UserTeam>? UserTeams { get; set; }
}