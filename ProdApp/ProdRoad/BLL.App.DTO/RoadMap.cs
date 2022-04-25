using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO;

public class RoadMap : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    
    [MaxLength(2)] 
    [Column(TypeName = "jsonb")]
    public LangStr? Position { get; set; } = new();
    
    [MaxLength(2)] 
    [Column(TypeName = "jsonb")]
    public LangStr? Line { get; set; } = new();

    public ICollection<Process>? Processes { get; set; }
}