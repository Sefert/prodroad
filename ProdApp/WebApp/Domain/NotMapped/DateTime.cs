using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Domain.NotMapped
{
    public class DateTime : Time
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public SqlDateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public SqlDateTime EndDate { get; set; }
    }
}