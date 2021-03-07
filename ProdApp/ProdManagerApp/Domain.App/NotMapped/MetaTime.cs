using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.App.NotMapped
{
    public abstract class MetaTime : BaseIdentity
    {
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime StartTime { get; set; } = default!;
        [DataType(DataType.Time)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:hh:mm}")]
        public DateTime? EndTime { get; set; }
    }
}