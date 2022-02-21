using Domain.Base;

namespace Domain.App;

public class CustomerPriceGroup : ModificationMeta
{
    public Guid PriceGroupId { get; set; }
    public PriceGroup PriceGroup { get; set; } = default!;

    public Guid CustomerId { get; set; }
    public Customer Customer { get; set; } = default!;
}