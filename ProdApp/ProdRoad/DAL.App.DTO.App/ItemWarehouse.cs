using Domain.Base;

namespace DAL.App.DTO;

public class ItemWarehouse : BaseEntity
{
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public ICollection<Process>? Processes { get; set; }
    public ICollection<Price>? Prices { get; set; }

    public decimal Quantity{ get; set; }
}