using System.ComponentModel.DataAnnotations;
using System.Data.SqlTypes;

namespace Domain.NotMapped
{
    public class DateTime : Time
    {
        [DataType(DataType.Date)]
        public SqlDateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public SqlDateTime EndDate { get; set; }
    }
}