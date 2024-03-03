using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp.Domain;

public class CustomerPrice : BaseEntity
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal Cost { get; set; }
}