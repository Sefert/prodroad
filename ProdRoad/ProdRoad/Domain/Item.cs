using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ProdRoad.Domain.Identity;

namespace ProdRoad.Domain;

public class Item : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser AppUser { get; set; } = default!;

    [MaxLength(80)] public string Name { get; set; } = default!;
    [MaxLength(30)] public string Type { get; set; } = default!;
    [MaxLength(30)] public string Unit { get; set; } = default;
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; } = default!;
    
    public ICollection<Price>? Prices { get; set; }
    public ICollection<CustomerPrice>? CustomerPrices { get; set; }
    public ICollection<OrderRow>? OrderRows { get; set; }
    public ICollection<ItemProcess>? ItemProcesses { get; set; }
}