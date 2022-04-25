using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Base;
using DAL.App.DTO.Identity;

namespace DAL.App.DTO;

public class Item : BaseEntity
{
    public Guid? ItemId { get; set; }
    public Item? ItemPart { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<Item>? Items { get; set; }
    public ICollection<Price>? Prices { get; set; }
    public ICollection<ItemWarehouse>? ItemWarehouses { get; set; }

    [MaxLength(80)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Name { get; set; } = new();
    
    [MaxLength(30)]  
    [Column(TypeName = "jsonb")]  
    public LangStr Type { get; set; } = new();
    
    [MaxLength(30)] 
    [Column(TypeName = "jsonb")] 
    public LangStr Unit { get; set; } = new();
    
    public decimal Quantity { get; set; }
}