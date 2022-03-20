using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class WarehouseDTO : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(50)] public string Address { get; set; } = default!;
    
    public ICollection<ItemWarehouse>? ItemWarehouses { get; set; }
}