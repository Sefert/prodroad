using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class RoadMap : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; } = default!;

    [MaxLength(30)] public string Name { get; set; } = default!;
    [MaxLength(2)] public string? Position { get; set; }
    [MaxLength(3)] public string? Line { get; set; }
    
    public ICollection<Process>? Processes { get; set; } = default!;
}