using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO;

public class ItemProcedure : BaseEntity
{
    public Guid ProcedureId { get; set; }
    public Procedure? Procedure { get; set; }
    
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    public bool CreatedUsed { get; set; }
    public decimal Quantity { get; set; }
}