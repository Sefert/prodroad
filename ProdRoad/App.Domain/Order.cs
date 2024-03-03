using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Order : BaseEntity
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; } = default!;
    
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }
    
    public DateTime Deadline { get; set; }
    
    public ICollection<OrderRow>? OrderRows { get; set; } = default!;
}

