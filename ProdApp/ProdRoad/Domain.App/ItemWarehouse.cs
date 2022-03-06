using Domain.App.Meta;

namespace Domain.App;

public class ItemWarehouse : UpdateMeta
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public Guid WarehouseId { get; set; }
    public Warehouse Warehouse { get; set; } = default!;

    public ICollection<Process>? Processes { get; set; }
    public ICollection<Price>? Prices { get; set; }

    public decimal Quantity{ get; set; } = default!;
}