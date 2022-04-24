using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DTO.App.Identity;

namespace DTO.App;

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