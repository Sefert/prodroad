using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using Domain.App.Identity;

namespace Domain.App;

public class Procedure : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(32)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = default!;
    
    [MaxLength(32)]
    [Column(TypeName = "jsonb")] 
    public LangStr Code { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<ItemProcedure>? ItemProcedures { get; set; }
}