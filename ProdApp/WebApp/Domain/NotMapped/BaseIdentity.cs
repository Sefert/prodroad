using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.NotMapped
{
    [NotMapped]
    public class BaseIdentity
    {
        public Guid Id { get; set; }
    }
}