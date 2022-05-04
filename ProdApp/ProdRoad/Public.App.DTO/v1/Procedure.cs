using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using Domain.Base;
using Public.App.DTO.v1.Identity;

namespace Public.App.DTO.v1;

public class Procedure : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(32)] public string Name { get; set; } = default!;
    [MaxLength(32)] public string Code { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<ItemProcedure>? ItemProcedures { get; set; }
}