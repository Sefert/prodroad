using Domain.Base;

namespace DTO.App;

public class CustomerPrice  : BaseEntity
{
    public Guid CustomerPriceGroupId { get; set; }
    public CustomerPriceGroup? CustomerPriceGroup { get; set; }

    public Guid PriceId { get; set; }
    public Price? Price{ get; set; }
}