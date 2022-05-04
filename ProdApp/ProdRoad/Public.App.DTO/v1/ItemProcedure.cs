using Domain.Base;

namespace Public.App.DTO.v1;

public class ItemProcedure : BaseEntity
{
    public Guid ProcedureId { get; set; }
    public Procedure? Procedure { get; set; }
    
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    public bool CreatedUsed { get; set; }
    public decimal Quantity { get; set; }
}