using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class RoadMapDTO : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(2)] public string? Position { get; set; }
    [MaxLength(2)] public string? Line { get; set; }

    public ICollection<Process>? Processes { get; set; }
}