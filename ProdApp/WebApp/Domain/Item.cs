using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class Item : BaseIdentity
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(30)]
        public string Type { get; set; }
        [MaxLength(30)]
        public string Unit { get; set; }
    }
}