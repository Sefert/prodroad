using Domain.Base;
using Domain.Base.Meta;

namespace Public.App.DTO.v1;

public class Price : UpdateMeta
{
    public Guid ItemId { get; set; }
    public Item? Item { get; set; }

    public Guid? ItemWarehouseId { get; set; }
    public ItemWarehouse? ItemWarehouse { get; set; }
    
    public decimal PureCost { get; set; }
    
    public ICollection<CustomerPrice>? CustomerPrices { get; set; }
}