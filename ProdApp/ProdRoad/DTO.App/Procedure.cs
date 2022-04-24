using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DTO.App.Identity;

namespace DTO.App;

public class Procedure : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(32)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    
    [MaxLength(32)]
    [Column(TypeName = "jsonb")] 
    public LangStr Code { get; set; } = new();

    public ICollection<Process>? Processes { get; set; }
    public ICollection<ItemProcedure>? ItemProcedures { get; set; }
}