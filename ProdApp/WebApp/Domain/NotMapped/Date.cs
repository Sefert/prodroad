using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlTypes;

namespace Domain.NotMapped
{
    [NotMapped]
    public class Date : BaseIdentity
    {
        [DataType(DataType.Date)]
        public SqlDateTime StartDate { get; set; }
        [DataType(DataType.Date)]
        public SqlDateTime EndDate { get; set; }
    }
}