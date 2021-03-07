using System;
using System.ComponentModel.DataAnnotations;

namespace Contracts.Domain.Base
{
    public interface IDomainEntityMetaDate
    {
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}