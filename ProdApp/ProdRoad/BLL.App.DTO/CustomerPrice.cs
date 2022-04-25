using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO;

public class CustomerPrice  : BaseEntity
{
    public Guid CustomerPriceGroupId { get; set; }
    public CustomerPriceGroup? CustomerPriceGroup { get; set; }

    public Guid PriceId { get; set; }
    public Price? Price{ get; set; }
}