using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.NotMapped
{
    public abstract class MetaDate : BaseIdentity
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public DateTime StartDate { get; set; } = default!;
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }
    }
}