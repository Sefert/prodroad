namespace ProdRoad.Domain;

public class CustomerPrice : BaseEntity
{
    public Guid ItemId { get; set; }
    public Item Item { get; set; } = default!;
    
    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
    
    public decimal Cost { get; set; }
}