using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class PriceGroupDTO : BaseEntity
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