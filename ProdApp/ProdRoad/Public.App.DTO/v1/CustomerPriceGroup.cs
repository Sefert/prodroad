using Domain.Base;
using Domain.Base.Meta;

namespace Public.App.DTO.v1;

public class CustomerPriceGroup : ModificationMeta
{
    public Guid PriceGroupId { get; set; }
    public PriceGroup? PriceGroup { get; set; }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public ICollection<CustomerPrice>? CustomerPrices { get; set; }
}