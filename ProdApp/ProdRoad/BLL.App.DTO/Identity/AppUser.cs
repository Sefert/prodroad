using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base.Identity;


namespace BLL.App.DTO.Identity;

/*TODO: fix firstname and lastname*/
public class AppUser : BaseUser
{
    [MaxLength(30)] public string? FirstName { get; set; }
    [MaxLength(30)] public string? LastName { get; set; }
    [MaxLength(30)] public string? PersonalCode { get; set; }
    
    /*[InverseProperty("CreatedBy")]
    public ICollection<CustomerPriceGroup>? CustomerPriceGroupCreatedBys { get; set; }
    [InverseProperty("UpdatedBy")]
    public ICollection<CustomerPriceGroup>? CustomerPriceGroupUpdatedBys { get; set; }
    
    [InverseProperty("UpdatedBy")]
    public ICollection<ItemWarehouse>? ItemWarehouseUpdatedBys { get; set; }
    
    [InverseProperty("UpdatedBy")]
    public ICollection<Price>? PriceUpdatedBys { get; set; }
    
    [InverseProperty("UpdatedBy")]
    public ICollection<Process>? ProcessUpdatedBys { get; set; }*/
    
    public ICollection<Team>? Teams { get; set; }
    public ICollection<RoadMap>? RoadMapItems { get; set; }
    public ICollection<Warehouse>? Warehouses { get; set; }
    public ICollection<Item>? Items { get; set; }
    public ICollection<UserTeam>? UserTeams { get; set; }
    public ICollection<Procedure>? Procedures { get; set; }
    public ICollection<PriceGroup>? PriceGroups { get; set; }
    public ICollection<Customer>? Customers { get; set; }
    public ICollection<Address>? Addresses { get; set; }
}