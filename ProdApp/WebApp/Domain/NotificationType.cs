using System.ComponentModel.DataAnnotations;
using Domain.NotMapped;

namespace Domain
{
    public class NotificationType : DateTime
    {
        [MaxLength(36)]
        public string Name { get; set; }
        [MaxLength(36)]
        public string Type { get; set; }
    }
}