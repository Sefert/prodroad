using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App;

public class RoadMap : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(2)] public string? Position { get; set; }
    [MaxLength(2)] public string? Line { get; set; }

    public ICollection<Process>? Processes { get; set; }
}