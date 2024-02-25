using System.ComponentModel.DataAnnotations;

namespace ProdRoad.Domain;

public class Price : BaseEntity
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
    
    public decimal Cost { get; set; } = default!;
    
    [MaxLength(80)] public string Region { get; set; } = default!;
}