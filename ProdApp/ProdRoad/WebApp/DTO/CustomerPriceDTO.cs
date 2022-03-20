using Domain.App;
using Domain.App.Meta;

namespace WebApp.DTO;

public class CustomerPriceDTO : ModificationMeta
{
    public Guid CustomerPriceGroupId { get; set; }
    public CustomerPriceGroup? CustomerPriceGroup { get; set; }

    public Guid PriceId { get; set; }
    public Price? Price{ get; set; }
}