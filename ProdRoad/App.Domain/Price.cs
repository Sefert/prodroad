using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Base.Domain;

namespace App.Domain;

public class Price : BaseEntity
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; } = default!;
    
    [MaxLength(80)] public string Region { get; set; } = default!;
}