using Domain.App;
using Domain.Base;

namespace WebApp.DTO;

public class ItemProcedureDTO : BaseEntity
{
    public Guid ProcedureId { get; set; }
    public Procedure? Procedure { get; set; }
    
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    public bool CreatedUsed { get; set; }
    public decimal Quantity { get; set; }
}