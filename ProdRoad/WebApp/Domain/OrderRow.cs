using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Domain;

public class OrderRow : BaseEntity
{
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = default!;
    
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public DateTime Deadline { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Quantity { get; set; }
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; }
    
    public ICollection<ItemProcess>? ItemProcesses { get; set; } = default!;
}