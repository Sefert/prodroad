using Domain.Base;

namespace DAL.App.DTO;

public class CustomerPriceGroup : BaseEntity
{
    public Guid PriceGroupId { get; set; }
    public PriceGroup? PriceGroup { get; set; }

    public Guid CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public ICollection<CustomerPrice>? CustomerPrices { get; set; }
}