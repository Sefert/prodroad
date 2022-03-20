using Domain.App;
using Domain.App.Meta;

namespace WebApp.DTO;

public class PriceDTO : UpdateMeta
{
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }

    public Guid? ItemWarehouseId { get; set; }
    public ItemWarehouse? ItemWarehouse { get; set; }
    
    public decimal PureCost { get; set; }
    
    public ICollection<CustomerPrice>? CustomerPrices { get; set; }
}