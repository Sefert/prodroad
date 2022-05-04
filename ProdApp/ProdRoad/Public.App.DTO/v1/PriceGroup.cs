using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Public.App.DTO.v1.Identity;

namespace Public.App.DTO.v1;

public class PriceGroup: BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid? PriceGroupId { get; set; }
    public PriceGroup? SubPriceGroup { get; set; }

    public ICollection<PriceGroup>? PriceGroups { get; set; }
    public ICollection<CustomerPriceGroup>? CustomerPriceGroups { get; set; }
    
    [MaxLength(30)] public string Name { get; set; } = default!;
    public decimal? Tax { get; set; }
    public decimal? Margin { get; set; }
    public decimal? Discount { get; set; }
}