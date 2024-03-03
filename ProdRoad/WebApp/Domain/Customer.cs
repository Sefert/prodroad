using System.ComponentModel.DataAnnotations;
using WebApp.Domain.Identity;

namespace WebApp.Domain;

public class Customer : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; } = default!;

    [MaxLength(50)] public string Name { get; set; } = default!;
    [MaxLength(50)] public string? Registration { get; set; }

    public ICollection<Address>? Addresses { get; set; } = default!;
    public ICollection<CustomerPrice>? CustomerPrice { get; set; }
    public ICollection<Order>? Order { get; set; }

}