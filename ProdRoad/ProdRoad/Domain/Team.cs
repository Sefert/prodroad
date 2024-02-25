using System.ComponentModel.DataAnnotations;
using ProdRoad.Domain.Identity;

namespace ProdRoad.Domain;

public class Team : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;

    [MaxLength(30)] public string Name { get; set; } = default!;
    [MaxLength(30)] public string Code { get; set; } = default!;
    
    public ICollection<Process>? Processes { get; set; } 
    public ICollection<UserTeam>? UserTeams { get; set; } 
}