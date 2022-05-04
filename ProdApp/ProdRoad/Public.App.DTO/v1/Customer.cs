using System.ComponentModel.DataAnnotations;
using Domain.Base;
using Public.App.DTO.v1.Identity;

namespace Public.App.DTO.v1;

public class Customer : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(50)] public string? Registration { get; set; }

    public ICollection<Address>? Addresses { get; set; }
    public ICollection<CustomerPriceGroup>? CustomerPriceGroups { get; set; }
}
