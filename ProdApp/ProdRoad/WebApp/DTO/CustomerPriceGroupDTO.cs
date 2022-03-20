using Domain.App;
using Domain.App.Meta;

namespace WebApp.DTO;

public class CustomerPriceGroupDTO : ModificationMeta
{
    public Guid PriceGroupId { get; set; }
    public PriceGroup? PriceGroup { get; set; }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public ICollection<CustomerPrice>? CustomerPrices { get; set; }
}