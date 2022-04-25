using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class Warehouse : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    
    [MaxLength(50)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Address { get; set; } = new();
    
    public ICollection<ItemWarehouse>? ItemWarehouses { get; set; }
}