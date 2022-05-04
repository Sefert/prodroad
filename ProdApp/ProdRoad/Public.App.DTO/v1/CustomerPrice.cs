using Domain.Base;
using Domain.Base.Meta;

namespace Public.App.DTO.v1;

public class CustomerPrice : ModificationMeta
{
    public Guid CustomerPriceGroupId { get; set; }
    public CustomerPriceGroup? CustomerPriceGroup { get; set; }

    public Guid PriceId { get; set; }
    public Price? Price{ get; set; }
}