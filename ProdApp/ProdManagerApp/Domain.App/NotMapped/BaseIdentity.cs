using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.App.NotMapped
{
    
    public abstract class BaseIdentity
    {
        public Guid Id { get; set; }
    }
}