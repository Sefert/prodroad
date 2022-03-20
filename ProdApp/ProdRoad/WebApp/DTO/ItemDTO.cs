using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class ItemDTO : BaseEntity
{
    public Guid? ItemId { get; set; }
    public Item? ItemPart { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<Item>? Items { get; set; }
    public ICollection<Price>? Prices { get; set; }
    public ICollection<ItemWarehouse>? ItemWarehouses { get; set; }

    [MaxLength(80)] public string Name { get; set; } = default!;
    [MaxLength(30)] public string Type { get; set; } = default!;
    [MaxLength(30)] public string Unit { get; set; } = default!;
    public decimal Quantity { get; set; }
}