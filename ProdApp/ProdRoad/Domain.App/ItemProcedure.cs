using Domain.App;
using Domain.Base;

namespace Domain.App;

public class ItemProcedure : BaseEntity
{
    public Guid ProcessId { get; set; }
    public Process Process { get; set; } = default!;
    
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public bool CreatedUsed { get; set; }
    public decimal Quantity { get; set; }
}