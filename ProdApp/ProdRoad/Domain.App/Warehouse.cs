using System.ComponentModel.DataAnnotations;

using Domain.Base;
using Domain.App.Identity;

namespace Domain.App;

public class Warehouse : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(50)] public string Address { get; set; } = default!;
    
    public ICollection<ItemWarehouse>? ItemWarehouses { get; set; }
}