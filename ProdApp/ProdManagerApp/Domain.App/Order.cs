using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.App.Identity;
using Domain.Base;

namespace Domain.App
{
    public class Order : DomainEntityId
    {
        [MaxLength(50)]
        public string Number { get; set; } = default!;
        [MaxLength(100)]
        public string Name { get; set; } = default!;
        [MaxLength(100)]
        public string DeliveryAddress { get; set; } = default!;
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DueDate { get; set; } = default!;
        [MaxLength(200)]
        public string? Info { get; set; }
        
        public ICollection<OrderData>? Supplys { get; set; }
        
        public Guid CustomerId { get; set; }
        public Customer? Customer { get; set; }
        
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser? ApplicationUser { get; set; }
    }
}