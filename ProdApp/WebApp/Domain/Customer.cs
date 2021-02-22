using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class Customer : BaseIdentity
    {
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [MaxLength(50)]
        public string RegNumber { get; set; } = default!;
        [MaxLength(100)]
        public string Address { get; set; } = default!;
        [MaxLength(20)]
        public string Phone { get; set; } = default!;
        [MaxLength(30)]
        public string Email { get; set; } = default!;
        
        public ICollection<Order>? Orders { get; set; }
    }
}