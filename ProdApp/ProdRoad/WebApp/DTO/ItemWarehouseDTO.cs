using Domain.App;
using Domain.App.Meta;

namespace WebApp.DTO;

public class ItemWarehouseDTO : UpdateMeta
{
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }
    
    public Guid WarehouseId { get; set; }
    public Warehouse? Warehouse { get; set; }

    public ICollection<Process>? Processes { get; set; }
    public ICollection<Price>? Prices { get; set; }

    public decimal Quantity{ get; set; }
}