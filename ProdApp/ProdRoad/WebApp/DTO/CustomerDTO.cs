using System.ComponentModel.DataAnnotations;
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class CustomerDTO : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(50)] public string? Registration { get; set; }

    public ICollection<Address>? Addresses { get; set; }
    public ICollection<CustomerPriceGroup>? CustomerPriceGroups { get; set; }
}
