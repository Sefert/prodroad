using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App;

public class Procedure : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(32)] public string Name { get; set; } = default!;
    [MaxLength(32)] public string Code { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<ItemProcedure>? ItemProcedures { get; set; }
}