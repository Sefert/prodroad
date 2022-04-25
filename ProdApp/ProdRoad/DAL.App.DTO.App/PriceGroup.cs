using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class PriceGroup : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid? PriceGroupId { get; set; }
    public PriceGroup? SubPriceGroup { get; set; }

    public ICollection<PriceGroup>? PriceGroups { get; set; }
    public ICollection<CustomerPriceGroup>? CustomerPriceGroups { get; set; }
    
    [MaxLength(30)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    public decimal? Tax { get; set; }
    public decimal? Margin { get; set; }
    public decimal? Discount { get; set; }
}