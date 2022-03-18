using Domain.App.Meta;
namespace Domain.App;

public class CustomerPrice : ModificationMeta
{
    public Guid CustomerPriceGroupId { get; set; }
    public CustomerPriceGroup? CustomerPriceGroup { get; set; }

    public Guid PriceId { get; set; }
    public Price? Price{ get; set; }
}