using System.ComponentModel.DataAnnotations;
using Base.Domain;
using WebApp.Domain.Identity;

namespace WebApp.Domain;

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
    [MaxLength(50)] public string? Email { get; set; }
    public DateTime From { get; set; }
    public DateTime? Until { get; set; }
}
