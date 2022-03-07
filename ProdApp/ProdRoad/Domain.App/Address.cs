using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App;

public class Address : BaseEntity
{
    public Guid? AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; }

    [MaxLength(30)] public string? Country { get; set; }
    [MaxLength(40)] public string? City { get; set; }
    [MaxLength(50)] public string? Street { get; set; }
    [MaxLength(30)] public string? Code { get; set; }
    [MaxLength(20)] public string? Phone { get; set; }
    //TODO: Remove if can
    [MaxLength(50)] public string? Email { get; set; }
}