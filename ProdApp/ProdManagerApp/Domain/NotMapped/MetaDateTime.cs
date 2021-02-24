using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.NotMapped
{
    public abstract class MetaDateTime : MetaTime
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public DateTime StartDate { get; set; }  = default!;
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public DateTime? EndDate { get; set; }
    }
}