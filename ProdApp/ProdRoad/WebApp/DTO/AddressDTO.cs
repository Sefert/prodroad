
using Domain.App;
using Domain.App.Identity;
using Domain.Base;

namespace WebApp.DTO;

public class AddressDTO : BaseEntity
{
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; } 
    
    //[Display(ResourceType = typeof(Resources.App.Domain.App.Address), Name = nameof(Country)) ]
    public string? Country { get; set; }
    
    public string? City { get; set; }
    
    public string? Street { get; set; }
    
    public string? Code { get; set; }
    
    public string? Phone { get; set; }

    public string? Email { get; set; }
}