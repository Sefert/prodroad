using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using BLL.App.DTO.Identity;
using Domain.Base;

namespace BLL.App.DTO;

public class Customer : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    
    [MaxLength(50)] 
    [Column(TypeName = "jsonb")] 
    public LangStr? Registration { get; set; } = new();

    public ICollection<Address>? Addresses { get; set; }
    public ICollection<CustomerPriceGroup>? CustomerPriceGroups { get; set; }
}