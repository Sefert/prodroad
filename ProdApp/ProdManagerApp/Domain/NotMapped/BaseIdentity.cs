using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.NotMapped
{
    
    public abstract class BaseIdentity
    {
        public Guid Id { get; set; }
    }
}