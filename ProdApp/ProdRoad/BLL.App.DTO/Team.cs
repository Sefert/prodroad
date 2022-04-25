using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO;

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

    public ICollection<Process>? Processes { get; set; } 
    public ICollection<UserTeam>? UserTeams { get; set; }
}