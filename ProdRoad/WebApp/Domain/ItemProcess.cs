namespace WebApp.Domain;

public class ItemProcess : BaseEntity
{
    public Guid? OrderRowId { get; set; }
    public OrderRow? OrderRow { get; set; }

    public Guid? ItemId { get; set; }
    public Item? Item { get; set; }
    
    public Guid? ProcessId { get; set; }
    public Process? Process { get; set; }
    
    public decimal Quantity { get; set; }
    public bool UsedCreated { get; set; }
    public bool Save { get; set; }
}