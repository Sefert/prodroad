using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Domain.NotMapped
{
    [NotMapped]
    public class Date : BaseIdentity
    {
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        public SqlDateTime StartDate { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:DD/mm/yyyy}")]
        [DataType(DataType.Date)]
        public SqlDateTime EndDate { get; set; }
    }
}