using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(30)] public string? FirstName { get; set; }
    [MaxLength(30)] public string? LastName { get; set; }
    [MaxLength(30)] public string? PersonalCode { get; set; }
    
    public ICollection<Team>? Teams { get; set; }
    public ICollection<RoadMap>? RoadMaps { get; set; }
    public ICollection<Item>? Items { get; set; }
    public ICollection<UserTeam>? UserTeams { get; set; }
    public ICollection<Process>? Processes { get; set; }
    public ICollection<Order>? Orders { get; set; }
    public ICollection<Customer>? Customers { get; set; }
    public ICollection<Address>? Addresses { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}