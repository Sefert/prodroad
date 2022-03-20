using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Customer : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(50)]
    [Column(TypeName = "jsonb")] public LangStr Name { get; set; } = default!;
    [MaxLength(50)] public LangStr? Registration { get; set; }

    public ICollection<Address>? Addresses { get; set; }
    public ICollection<CustomerPriceGroup>? CustomerPriceGroups { get; set; }
}