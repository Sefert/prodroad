using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DAL.App.DTO.Identity;


namespace DAL.App.DTO;

public class Address : BaseEntity
{
    public Guid AppUserId { get; set; }
    /*public AppUser? AppUser { get; set; }*/
    
    public Guid? CustomerId { get; set; }
    public Customer? Customer { get; set; } 

    [MaxLength(30)]
    [Column(TypeName = "jsonb")] 
    public LangStr? Country { get; set; } = new();
    
    [MaxLength(40)]
    [Column(TypeName = "jsonb")] 
    public LangStr? City { get; set; } = new();
    
    [MaxLength(50)]
    [Column(TypeName = "jsonb")] 
    public LangStr? Street { get; set; } = new();
    
    [MaxLength(30)] 
    [Column(TypeName = "jsonb")]
    public LangStr? Code { get; set; }= new();

    [MaxLength(20)]
    [Column(TypeName = "jsonb")]
    public LangStr? Phone { get; set; } = new();
    
    [MaxLength(50)] 
    [Column(TypeName = "jsonb")]
    public LangStr? Email { get; set; }= new();
}