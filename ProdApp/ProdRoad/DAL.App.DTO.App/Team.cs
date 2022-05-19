using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class Team : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    
    [MaxLength(20)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Code { get; set; } = new();
    
    [MaxLength(20)] public bool IsPublic { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; } 
    public ICollection<UserTeam>? UserTeams { get; set; }
}